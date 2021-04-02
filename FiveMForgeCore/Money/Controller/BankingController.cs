using System;
using System.Collections.Generic;
using CitizenFX.Core;
using FiveMForge.database;
using FiveMForgeCore.Models;
using MySqlConnector;

namespace FiveMForgeCore.Money.Controller
{
    public class BankingController : BaseClass
    {
        public BankingController()
        {
            EventHandlers[ServerEvents.LoadBankLocations] += new Action<Player>(OnBankLocationsRequested);
        }

        private async void OnBankLocationsRequested([FromSource] Player player)
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
            var bankList = new List<BankInformation>();
            while (reader.Read())
            {
                var name = reader.GetString("name");
                var isActive = reader.GetBoolean("isActive");
                var isAdminOnly = reader.GetBoolean("isAdminOnly");
                var location = reader.GetString("location");
                var locationVector = location.Split(':');
                bankList.Add(new (name, 108, float.Parse(locationVector[0]), float.Parse(locationVector[1]), float.Parse(locationVector[2]), isActive, isAdminOnly));
            }
            
            TriggerClientEvent(player, ServerEvents.BankLocationsLoaded, bankList.ToArray());
        }
    }
}