using System;
using System.Collections.Generic;
using CitizenFX.Core;
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
    }
}