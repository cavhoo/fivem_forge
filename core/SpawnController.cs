using System;
using System.Globalization;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FiveMForge.database;
using MySqlConnector;

namespace FiveMForge
{
    public class SpawnController : BaseScript
    {
        public SpawnController()
        {
            EventHandlers["playerConnecting"] += new Action<Player, string, dynamic, dynamic>(OnPlayerConnecting);
            EventHandlers["playerDropped"] += new Action<Player, string>(OnPlayerDisconnecting);
        }

        private async void OnPlayerConnecting([FromSource] Player player, string playerName, dynamic setKickReason,
            dynamic deferrals)
        {
            var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0); // Get first identifier
            Debug.WriteLine($"Player Joined: {playerIdentifier}");
            // Check if player has an entry in database
            using (var db = new DbConnector())
            {
                await db.Connection.OpenAsync();
                using var playerExistsCommand = new MySqlCommand();
                playerExistsCommand.Connection = db.Connection;
                playerExistsCommand.CommandText = $"SELECT * FROM players where account_id = '{playerIdentifier}'";
                using var reader = await playerExistsCommand.ExecuteReaderAsync();
                await reader.ReadAsync();
                if (reader.HasRows) return;
            }

            // Player joined for the first time, create new entry in player database
            using (var db = new DbConnector())
            {
                await db.Connection.OpenAsync();
                var newPlayerUuid = Guid.NewGuid();
                // Save character to database
                using var playerCreateCommand = new MySqlCommand();
                playerCreateCommand.Connection = db.Connection;
                playerCreateCommand.CommandText =
                    $"insert into players (account_id, uuid, last_login) values ('{playerIdentifier}', '{newPlayerUuid}', '{DateTime.Now.ToUniversalTime().ToString(CultureInfo.InvariantCulture)}')";
                await playerCreateCommand.ExecuteNonQueryAsync();
                using var characterCreateCommand = new MySqlCommand();
                characterCreateCommand.Connection = db.Connection;
                characterCreateCommand.CommandText =
                    $"insert into characters (uuid, in_use, last_pos) values ('{newPlayerUuid}', {true}, '')";
                await characterCreateCommand.ExecuteNonQueryAsync();
            }
        }

        private async void OnPlayerDisconnecting([FromSource] Player player, string reason)
        {
            // Save player position when he leaves the server
            using (var db = new DbConnector())
            {
                await db.Connection.OpenAsync();
                var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);
                using var getPlayerCommand = new MySqlCommand();
                getPlayerCommand.Connection = db.Connection;
                getPlayerCommand.CommandText = $"select uuid from players where account_id = '{playerIdentifier}'";
                using var reader = await getPlayerCommand.ExecuteReaderAsync();
                await reader.ReadAsync();
                if (!reader.HasRows) return;
                using (var db2 = new DbConnector())
                {
                    var playerUuid = reader.GetString(0);
                    using var savePlayerPosCommand = new MySqlCommand();
                    savePlayerPosCommand.Connection = db.Connection;
                    savePlayerPosCommand.CommandText =
                        $"update characters set last_pos='{player.Character.Position.ToString()}' where uuid='{playerUuid}' and in_use=true";
                    await savePlayerPosCommand.ExecuteNonQueryAsync();
                }
            }
            

        }
    }
}