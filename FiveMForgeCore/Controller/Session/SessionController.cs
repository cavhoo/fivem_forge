using System;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FiveMForge.Controller.Base;
using FiveMForge.Database;
using MySqlConnector;

namespace FiveMForge.Controller.Session
{
    public class SessionController : BaseClass
    {
        public SessionController()
        {
            EventHandlers["FiveMForge:GetSessionId"] += new Action<Player>(GetSessionId);
            EventHandlers["playerDropped"] += new Action<Player>(ClearSessionId);
        }

        private async void GetSessionId([FromSource] Player player)
        {
            var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);
            using (var playerDBConnection = new DbConnector())
            {
                await playerDBConnection.Connection.OpenAsync();
                var playerDBCommand = new MySqlCommand();
                playerDBCommand.Connection = playerDBConnection.Connection;
                var sessionId = Guid.NewGuid();
                playerDBCommand.CommandText = $"update players set sessionId='{sessionId}' where account_id={playerIdentifier}";
                await playerDBCommand.ExecuteNonQueryAsync();
                player.TriggerEvent("FiveMForge:SessionIdLoaded", sessionId);
            }
        }

        private async void ClearSessionId([FromSource] Player player)
        {
            var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);
            using (var playerDBConnection = new DbConnector())
            {
                await playerDBConnection.Connection.OpenAsync();
                var playerDBCommand = new MySqlCommand();
                playerDBCommand.Connection = playerDBConnection.Connection;
                playerDBCommand.CommandText = $"update players set sessionId=null where account_id={playerIdentifier}";
                await playerDBCommand.ExecuteNonQueryAsync();
            }
        }
    }
}