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

namespace FiveMForge.Controller.Money
{
    public class BankingController : BaseClass
    {
        public BankingController()
        {
            EventHandlers[ServerEvents.LoadBankLocations] += new Action<Player, string>(OnBankLocationsRequested);
            EventHandlers[ServerEvents.LoadBankAccount] += new Action<Player>(OnRequestBankAccount);
            EventHandlers[ServerEvents.LoadWallet] += new Action<Player>(OnRequestWallet);
            
        }

        private async void OnBankLocationsRequested([FromSource] Player player, string sessionId)
        {
            using var ctx = new CoreContext();
            var banks = ctx.Banks.Where(b => b.IsActive).ToList();

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

        private async void OnRequestBankAccount([FromSource] Player player)
        {
            using var ctx = new CoreContext();
            var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);
            var currentPlayer = ctx.Players.FirstOrDefault(p => p.AccountId == playerIdentifier);

            if (currentPlayer == null) return;

            var bankAccount = ctx.BankAccount.FirstOrDefault(b => b.Holder == currentPlayer.Uuid);
            if (bankAccount == null) return;
            player.TriggerEvent(ServerEvents.BankAccountLoaded, bankAccount.Saldo);
        }

        private async void OnRequestWallet([FromSource] Player player)
        {
            // TODO: Refactor this to use the character UUID instead. So we have multiple wallets.
            using var ctx = new CoreContext();
            var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);
            var currentPlater = ctx.Players.FirstOrDefault(p => p.AccountId == playerIdentifier);

            if (currentPlater == null) return;
            var wallet = ctx.Wallets.FirstOrDefault(w => w.Holder == currentPlater.Uuid);
            
            if (wallet == null) return;
            
            player.TriggerEvent(ServerEvents.WalletLoaded, wallet.Saldo);
        }
    }
}