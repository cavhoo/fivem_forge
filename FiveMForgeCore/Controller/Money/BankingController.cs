using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FiveMForge.Controller.Base;
using FiveMForge.Database;
using FiveMForge.Database.Contexts;
using FiveMForge.Models;
using MySqlConnector;
using Newtonsoft.Json;
using Player = CitizenFX.Core.Player;

namespace FiveMForge.Controller.Money
{
    /// <summary>
    /// Class <c>BankingController</c>
    /// Controls the interactions when accessing Bank related stuff
    /// inside the client, from the phone or from a teller.
    /// Also send the location information about the Banks to the client.
    /// </summary>
    public class BankingController : BaseClass
    {
        public BankingController()
        {
            EventHandlers[ServerEvents.LoadBankLocations] += new Action<Player, string>(OnBankLocationsRequested);
            EventHandlers[ServerEvents.LoadBankAccount] += new Action<Player>(OnRequestBankAccount);
            EventHandlers[ServerEvents.LoadWallet] += new Action<Player>(OnRequestWallet);
            
        }

        private void OnBankLocationsRequested([FromSource] Player player, string sessionId)
        {
            var banks = Context.Banks.Where(b => b.IsActive).ToList();

            var bankListDto = new List<dynamic>();

            foreach (var bank in banks)
            {
                var locSplit = bank.Location.Split(':');
                var bankInfo = new BankInformation();
                bankInfo.Name = bank.Name;
                bankInfo.IsActive = bank.IsActive;
                bankInfo.SpriteId = 88;
                bankInfo.IsAdminOnly = bank.IsAdminOnly;
                bankInfo.X = float.Parse(locSplit[0]);
                bankInfo.Y = float.Parse(locSplit[1]);
                bankInfo.Z = float.Parse(locSplit[2]);
            }
            player.TriggerEvent(ServerEvents.BankLocationsLoaded, JsonConvert.SerializeObject(bankListDto));
        }

        private void OnRequestBankAccount([FromSource] Player player)
        {
            var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);
            var currentPlayer = Context.Players.FirstOrDefault(p => p.AccountId == playerIdentifier);

            if (currentPlayer == null) return;

            var bankAccount = Context.BankAccount.FirstOrDefault(b => b.Holder == currentPlayer.Uuid);
            if (bankAccount == null) return;
            player.TriggerEvent(ServerEvents.BankAccountLoaded, bankAccount.Saldo);
        }

        private void OnRequestWallet([FromSource] Player player)
        {
            // TODO: Refactor this to use the character UUID instead. So we have multiple wallets.
            var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);
            var currentPlayer = Context.Players.FirstOrDefault(p => p.AccountId == playerIdentifier);

            if (currentPlayer == null) return;
            
            var activeCharacter =
                Context.Characters.FirstOrDefault(c => c.AccountUuid == currentPlayer.Uuid && c.InUse);
            
            if (activeCharacter == null) return;
            var wallet = Context.Wallets.FirstOrDefault(w => w.Holder == activeCharacter.Uuid);
            
            if (wallet == null) return;
            
            player.TriggerEvent(ServerEvents.WalletLoaded, wallet.Saldo);
        }
    }
}