using System;
using System.Globalization;
using System.Linq;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FiveMForge.Controller.Base;
using FiveMForge.Database;
using FiveMForge.Database.Contexts;
using FiveMForge.Database.Models;
using FiveMForge.Models;
using MySqlConnector;
using Player = CitizenFX.Core.Player;

namespace FiveMForge.Controller.Spawn
{
    public class SpawnController : BaseClass
    {
        public SpawnController()
        {
            EventHandlers["playerConnecting"] += new Action<Player, string, dynamic, dynamic>(OnPlayerConnecting);
            EventHandlers["playerDropped"] += new Action<Player, string>(OnPlayerDisconnecting);
            EventHandlers["FiveMForge:GetLastSpawnPosition"] += new Action<Player, string>(OnGetLastPlayerPosition);
        }

        private async void OnPlayerConnecting([FromSource] Player player, string playerName, dynamic setKickReason,
            dynamic deferrals)
        {
            var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0); // Get first identifier
            Debug.WriteLine($"Player Joined: {playerIdentifier}");

            using var ctx = new CoreContext();

            var currentPlayer = ctx.Players.FirstOrDefault(p => p.AccountId == playerIdentifier);
            // If we have a player we don't have to continue, we only update the login time.
            if (currentPlayer != null)
            {
                currentPlayer.LastLogin = DateTime.Now.ToUniversalTime().ToString(CultureInfo.CurrentCulture);
                await ctx.SaveChangesAsync();
                return;
            }

            // Create new Player Database Model.
            var newPlayer = new Database.Models.Player();
            // Every character has a UUID which is used to reference the account.
            // This way we can transfer user accounts to other Platforms: Steam <> Epic <> Rockstar
            newPlayer.Uuid = Guid.NewGuid().ToString();
            newPlayer.AccountId = playerIdentifier;
            newPlayer.LastLogin = DateTime.Now.ToUniversalTime().ToString(CultureInfo.CurrentCulture);
            ctx.Players.Add(newPlayer);

                
            var newCharacter = new Character();
            newCharacter.Job = null;
            newCharacter.Uuid = newPlayer.Uuid;
            newCharacter.CharacterUuid = Guid.NewGuid().ToString();
            newCharacter.InUse = true;
            ctx.Characters.Add(newCharacter);
            await ctx.SaveChangesAsync();
        }

        private async void OnPlayerDisconnecting([FromSource] Player player, string reason)
        {
            // Get AccountID from FiveM
            var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);
            Debug.WriteLine($"Disconnecting: {playerIdentifier}");
            using var ctx = new CoreContext();
            
            // Get Matching player in Database.
            var currentPlayer = ctx.Players.FirstOrDefault(p => p.AccountId == playerIdentifier);
            if (currentPlayer == null)
            {
                return;
            }

            // Grab the last character position, if there's none we use 0:0:0
            var lastPosition = player.Character?.Position ?? Vector3.Zero;
            var character = ctx.Characters.FirstOrDefault(c => c.Uuid == currentPlayer.Uuid);
            if (character == null)
            {
                return;
            }

            // Convert Position to our string format :)
            character.LastPos = $"{lastPosition.X}:{lastPosition.Y}:{lastPosition.Z}";
            await ctx.SaveChangesAsync();
        }

        private async void OnGetLastPlayerPosition([FromSource] Player player, string sessionId)
        {
            var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);
            using var ctx = new CoreContext();

            var currentPlayer = ctx.Players.FirstOrDefault(p => p.AccountId == playerIdentifier);
            if (currentPlayer == null) 
            {
                // TODO: Send error to client to say that there's no account.
                return;
            }

            var character = ctx.Characters.FirstOrDefault(c => c.Uuid == currentPlayer.Uuid && c.InUse);
            if (character == null)
            {
                // TODO: Send error message to client if no character has been found.
                // This should never happen though xD
                return;
            }
            var posArray = character?.LastPos.Split(':');
            player.TriggerEvent("FiveMForge:SpawnAt", float.Parse(posArray[0]), float.Parse(posArray[1]), float.Parse(posArray[2]));
        }
    }
}