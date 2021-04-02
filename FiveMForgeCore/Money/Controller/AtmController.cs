using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using FiveMForge.database;
using FiveMForgeCore.Models;
using MySqlConnector;
using static CitizenFX.Core.Native.API;

namespace FiveMForgeCore.Money.Controller
{
    public class AtmController : BaseClass
    {
        public AtmController()
        {
            EventHandlers[ServerEvents.LoadAtmLocations] += new Action<Player>(OnAtmLocationsRequested);
        }

        private async void OnAtmLocationsRequested([FromSource] Player player)
        {
            Debug.WriteLine("Handling request to load atm locations...");
            using var connector = new DbConnector();
            await connector.Connection.OpenAsync();
            var loadAtmCommand = new MySqlCommand();
            loadAtmCommand.CommandText = "select * from atms";
            loadAtmCommand.Connection = connector.Connection;
            var reader = await loadAtmCommand.ExecuteReaderAsync();
            await reader.ReadAsync();
            if (!reader.HasRows) return;
            var atmlocations = new List<Vector3>();
            while (reader.Read())
            {
                var row = reader.GetString("location");
                var rowSplit = row.Split(':');
                atmlocations.Add(new Vector3(float.Parse(rowSplit[0]), float.Parse(rowSplit[1]), float.Parse(rowSplit[2])));
            }
            
            TriggerClientEvent(player, ServerEvents.AtmLocationsLoaded, atmlocations.ToArray());
        }
    }
}