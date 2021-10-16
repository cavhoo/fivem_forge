using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FiveMForge.Controller.Base;
using FiveMForge.Database;
using FiveMForge.Models;
using FiveMForge.Models.Errors;
using FiveMForge.Utils;
using Newtonsoft.Json;
using Player = CitizenFX.Core.Player;

namespace FiveMForge.Controller.Money
{
    /// <summary>
    /// Class <c>AtmController</c>
    /// Controls the commands available at ATMs, from
    /// withdrawing money to making deposits. Also send
    /// the information about each atm location to the
    /// client.
    /// </summary>
    public class AtmController : BaseClass
    {
        public AtmController()
        {
            EventHandlers[ClientEvents.LoadAtmLocations] += new Action<Player>(OnAtmLocationsRequested);
            EventHandlers[ClientEvents.WithDrawMoney] += new Action<Player, string, int>(OnWithdrawMoney);
            EventHandlers[ClientEvents.DepositMoney] += new Action<Player, string, int>(OnDepositMoney);
        }

        private void OnDepositMoney([FromSource] Player player, string characterUuid, int amount)
        {
            var account =
                Context.Players.FirstOrDefault(p => p.AccountId == API.GetPlayerIdentifier(player.Handle, 0));

            if (account != null)
            {
                var currentCharacter = Context.Characters.FirstOrDefault(c => c.AccountUuid == account.AccountUuid);
                if (currentCharacter != null)
                {
                    var characterWallet =
                        Context.Wallets.FirstOrDefault(w => w.Holder == currentCharacter.CharacterUuid);
                    if (characterWallet != null)
                    {
                        if (characterWallet.Saldo >= amount)
                        {
                            var bankAccount =
                                Context.BankAccount.FirstOrDefault(a => a.Holder == currentCharacter.CharacterUuid);
                            if (bankAccount != null)
                            {
                                bankAccount.Saldo += amount;
                                characterWallet.Saldo -= amount;
                                Context.SaveChangesAsync();
                            }
                            else
                            {
                                player.TriggerEvent(ServerEvents.Error, BankErrors.AccountNotFound);
                            }
                        }
                        else
                        {
                            player.TriggerEvent(ServerEvents.Error, WalletErrors.NotEnoughBalance);
                        }
                    }
                    else
                    {
                        player.TriggerEvent(ServerEvents.Error, WalletErrors.NotFound);
                    }
                }
                else
                {
                    player.TriggerEvent(ServerEvents.Error, CharacterErrors.CharacterNotFound);
                }
            }
            else
            {
                player.TriggerEvent(ServerEvents.Error, AccountErrors.NotFound);
            }
        }

        private void OnAtmLocationsRequested([FromSource] Player player)
        {
            var atmLocations = Context.Atms.Select(a => a.Location).ToArray();
            TriggerClientEvent(player, ServerEvents.AtmLocationsLoaded, JsonConvert.SerializeObject(atmLocations));
        }

        private void OnWithdrawMoney([FromSource] Player player, string characterUuid, int amount)
        {
            var playerId = API.GetPlayerIdentifier(player.Handle, 0);
            var playerAccount = Context.Players.FirstOrDefault(p => p.AccountId == playerId);
            if (playerAccount != null)
            {
                var character = Context.Characters.FirstOrDefault(c => c.AccountUuid == playerAccount.AccountUuid);
                if (character != null)
                {
                    var account = Context.BankAccount.FirstOrDefault(a => a.Holder == character.CharacterUuid);
                    if (account != null)
                    {
                        if (account.Saldo >= amount)
                        {
                            var wallet = Context.Wallets.FirstOrDefault(w => w.Holder == character.CharacterUuid);
                            if (wallet != null)
                            {
                                account.Saldo -= amount;
                                wallet.Saldo += amount;
                                Context.SaveChangesAsync();
                            }
                            player.TriggerEvent(ServerEvents.MoneyWithdrawn);
                        }
                        else
                        {
                            player.TriggerEvent(ServerEvents.Error, AtmErrors.NoEnoughBalance);
                        }
                    }
                    else
                    {
                        player.TriggerEvent(ServerEvents.Error, AtmErrors.AccountNotFound);
                    }
                }
                else
                {
                    player.TriggerEvent(ServerEvents.Error, CharacterErrors.CharacterNotFound);
                }
            }
            else
            {
                player.TriggerEvent(ServerEvents.Error, AccountErrors.NotFound);
            }
        }
    }
}