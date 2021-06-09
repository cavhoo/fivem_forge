using System;
using System.Globalization;
using System.Linq;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FiveMForge.Controller.Base;
using FiveMForge.Database;
using FiveMForge.Database.Contexts;
using FiveMForge.Models;
using MySqlConnector;
using Player = CitizenFX.Core.Player;

namespace FiveMForge.Controller.Spawn
{
    public class SpawnController : BaseClass
    {
        /// <summary>
        /// Class <c>SpawnController</c>
        /// Handles everything relating to spawning a character.
        /// For example if a character just joined the server he will request the last position,
        /// that he was at when logged out.
        /// If a character dies we will respawn him at the closest hospital.
        /// </summary>
        public SpawnController()
        {
            EventHandlers["FiveMForge:GetLastSpawnPosition"] += new Action<Player, string>(OnGetLastPlayerPosition);
        }

        private void OnGetLastPlayerPosition([FromSource] Player player, string sessionId)
        {
            var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);

            var currentPlayer = Context.Players.FirstOrDefault(p => p.AccountId == playerIdentifier);
            if (currentPlayer == null) 
            {
                // TODO: Send error to client to say that there's no account.
                return;
            }

            var character = Context.Characters.FirstOrDefault(c => c.Uuid == currentPlayer.Uuid && c.InUse);
            if (character == null)
            {
                // TODO: Send error message to client if no character has been found.
                // This should never happen though xD
                player.TriggerEvent("Five");
                return;
            }
            var posArray = character?.LastPos.Split(':');
            player.TriggerEvent("FiveMForge:SpawnAt", float.Parse(posArray[0]), float.Parse(posArray[1]), float.Parse(posArray[2]));
        }
    }
}