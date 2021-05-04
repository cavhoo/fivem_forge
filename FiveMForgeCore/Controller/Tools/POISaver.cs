using System;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FiveMForge.Controller.Base;
using FiveMForge.Database;
using FiveMForge.Models;
using MySqlConnector;

namespace FiveMForge.Controller.Tools
{
    public class POISaver : BaseClass
    {
        public POISaver()
        {
            EventHandlers[ServerEvents.SavePOIPosition] += new Action<Player, string>(OnSavePOIPosition);
        }

        private async void OnSavePOIPosition([FromSource] Player player, string type)
        {
            if (type == "Unkown") return;
            var currentPosition = player.Character?.Position ?? Vector3.Zero;
            using (var db = new DbConnector())
            {
                await db.Connection.OpenAsync();
                var savePoiPosition = new MySqlCommand();
                savePoiPosition.Connection = db.Connection;
                savePoiPosition.CommandText = $"insert into poi (type, x, y, z) values('{type}',{currentPosition.X},{currentPosition.Y},{currentPosition.Z})";
                await savePoiPosition.ExecuteNonQueryAsync();
            }
        }
    }
}