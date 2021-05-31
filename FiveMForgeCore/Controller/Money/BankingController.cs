using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FiveMForge.Controller.Base;
using FiveMForge.Database;
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
            Debug.WriteLine("Handling request to load Bank locations...");
            using var db = new DbConnector();
            await db.Connection.OpenAsync();

            var bankLocationCommand = new MySqlCommand();
            bankLocationCommand.CommandText = "select * from banks";
            bankLocationCommand.Connection = db.Connection;
            var reader = await bankLocationCommand.ExecuteReaderAsync();
            await reader.ReadAsync();
            if (!reader.HasRows) return;
            var bankList = new List<dynamic>();
            while (reader.Read())
            {
                var name = reader.GetString("name");
                var isActive = reader.GetBoolean("isActive");
                var isAdminOnly = reader.GetBoolean("isAdminOnly");
                var location = reader.GetString("location");
                var locationVector = location.Split(':');

                var bank = new BankInformation();
                bank.Name = name;
                bank.IsActive = isActive;
                bank.IsAdminOnly = isAdminOnly;
                bank.X = float.Parse(locationVector[0]);
                bank.Y = float.Parse(locationVector[1]);
                bank.Z = float.Parse(locationVector[2]);
                bankList.Add(bank);
            }
            Debug.WriteLine($"Sending banking locations to client, counting: {bankList.Count} banks");
            player.TriggerEvent(ServerEvents.BankLocationsLoaded, JsonConvert.SerializeObject(bankList));
        }

        private async void OnRequestBankAccount([FromSource] Player player)
        {
            using (var connection = new DbConnector())
            {
                var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);
                await connection.Connection.OpenAsync();

                var getPlayerUuidCommand = new MySqlCommand();
                getPlayerUuidCommand.Connection = connection.Connection;
                getPlayerUuidCommand.CommandText = $"select uuid from players where account_id={playerIdentifier}";
                var reader = await getPlayerUuidCommand.ExecuteReaderAsync();
                if (!reader.HasRows) return;
                var playerUuid = reader.GetString(0);
                
                var getBankAccountCommand = new MySqlCommand();
                getBankAccountCommand.Connection = connection.Connection;
                getBankAccountCommand.CommandText = $"select * from bankAccount where holder={playerUuid}";
                var accountReader = await getBankAccountCommand.ExecuteReaderAsync();
                if (!accountReader.HasRows) return;

                var saldo = accountReader.GetDecimal("saldo");
                player.TriggerEvent(ServerEvents.BankAccountLoaded, saldo);
            }
        }

        private async void OnRequestWallet([FromSource] Player player)
        {
            using (var connection = new DbConnector())
            {
                var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);
                await connection.Connection.OpenAsync();

                var getPlayerUuidCommand = new MySqlCommand();
                getPlayerUuidCommand.CommandText = $"select uuid from players where account_id={playerIdentifier}";
                getPlayerUuidCommand.Connection = connection.Connection;

                var playerUuidReader = await getPlayerUuidCommand.ExecuteReaderAsync();
                if (!playerUuidReader.HasRows) return;

                var playerUuid = playerUuidReader.GetString(0);

                var getWalletCommand = new MySqlCommand();
                getWalletCommand.Connection = connection.Connection;
                getWalletCommand.CommandText = $"select * from wallets where holder={playerUuid}";

                var walletReader = await getWalletCommand.ExecuteReaderAsync();
                if (!walletReader.HasRows) return;

                var saldo = walletReader.GetDecimal(0);
                player.TriggerEvent(ServerEvents.WalletLoaded, saldo);
            }
        }
    }
}