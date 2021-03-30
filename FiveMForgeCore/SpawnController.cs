using System;
using System.Globalization;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FiveMForge.database;
using MySqlConnector;

namespace FiveMForgeCore
{
    public class SpawnController : BaseClass
    {
        public SpawnController()
        {
            EventHandlers["playerConnecting"] += new Action<Player, string, dynamic, dynamic>(OnPlayerConnecting);
            EventHandlers["playerDropped"] += new Action<Player, string>(OnPlayerDisconnecting);
            EventHandlers["FiveMForge:GetLastSpawnPosition"] += new Action<Player>(OnGetLastPlayerPosition);
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
                playerExistsCommand.CommandText = $"SELECT uuid FROM players where account_id = '{playerIdentifier}'";
                using var reader = await playerExistsCommand.ExecuteReaderAsync();
                await reader.ReadAsync();
                if (reader.HasRows) return;
                // Player joined for the first time, create new entry in player database
                using (var db2 = new DbConnector())
                {
                    await db2.Connection.OpenAsync();
                    var newPlayerUuid = Guid.NewGuid();
                    // Save character to database
                    using var playerCreateCommand = new MySqlCommand();
                    playerCreateCommand.Connection = db2.Connection;
                    playerCreateCommand.CommandText =
                        $"insert into players (account_id, uuid, last_login) values ('{playerIdentifier}', '{newPlayerUuid}', '{DateTime.Now.ToUniversalTime().ToString(CultureInfo.InvariantCulture)}')";
                    await playerCreateCommand.ExecuteNonQueryAsync();
                    using var characterCreateCommand = new MySqlCommand();
                    characterCreateCommand.Connection = db2.Connection;
                    characterCreateCommand.CommandText =
                        $"insert into characters (uuid, in_use, last_pos) values ('{newPlayerUuid}', {true}, '')";
                    await characterCreateCommand.ExecuteNonQueryAsync();
                }
            }
        }

        private async void OnPlayerDisconnecting([FromSource] Player player, string reason)
        {
            var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);
            Debug.WriteLine($"Disconnecting: {playerIdentifier}");
            // Save player position when he leaves the server
            using var db = new DbConnector();
            var lastPlayerPosition = player.Character?.Position ?? Vector3.Zero;
            await db.Connection.OpenAsync();
            using var getPlayerCommand = new MySqlCommand();
            getPlayerCommand.Connection = db.Connection;
            getPlayerCommand.CommandText = $"select uuid from players where account_id = '{playerIdentifier}'";
            using var reader = await getPlayerCommand.ExecuteReaderAsync();
            await reader.ReadAsync();
            if (!reader.HasRows) return;
            using var db2 = new DbConnector();
            await db2.Connection.OpenAsync();
            var playerUuid = reader.GetString(0);

            var lastPosString = $"{lastPlayerPosition.X}:{lastPlayerPosition.Y}:{lastPlayerPosition.Z}";

            using var savePlayerPosCommand = new MySqlCommand();
            savePlayerPosCommand.Connection = db2.Connection;
            savePlayerPosCommand.CommandText =
                $"update characters set last_pos='{lastPosString}' where uuid='{playerUuid}' and in_use=true";
            await savePlayerPosCommand.ExecuteNonQueryAsync();
        }

        private async void OnGetLastPlayerPosition([FromSource] Player player)
        {
            var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);
            using (var db = new DbConnector())
            {
                await db.Connection.OpenAsync();
                using var playerExistsCommand = new MySqlCommand();
                playerExistsCommand.Connection = db.Connection;
                playerExistsCommand.CommandText = $"SELECT uuid FROM players where account_id = '{playerIdentifier}'";
                using var reader = await playerExistsCommand.ExecuteReaderAsync();
                await reader.ReadAsync();

                if (!reader.HasRows) return;

                var playerUuid = reader.GetString(0); // Retrieve UUID from first entry
                using (var characterConnector = new DbConnector())
                {
                    await characterConnector.Connection.OpenAsync();
                    using var selectCharacterCommand = new MySqlCommand();
                    selectCharacterCommand.Connection = characterConnector.Connection;
                    selectCharacterCommand.CommandText = $"select last_pos from characters where uuid='{playerUuid}'";
                    var characterReader = await selectCharacterCommand.ExecuteReaderAsync();
                    await characterReader.ReadAsync();
                    if (!characterReader.HasRows) return; // No pos found returning

                    // Parse position string into 3 float array [x, y, z]
                    var posArray = characterReader.GetString(0).Split(':');
                    // Send last position to FiveMForgeClient to adjust spawn location
                    player.TriggerEvent("FiveMForge:SpawnAt", float.Parse(posArray[0]), float.Parse(posArray[1]), float.Parse(posArray[2]));
                }
            }
        }
    }
}