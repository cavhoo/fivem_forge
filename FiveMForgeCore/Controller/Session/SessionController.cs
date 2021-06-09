using System;
using System.Globalization;
using System.Linq;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FiveMForge.Controller.Base;
using FiveMForge.Database;
using FiveMForge.Database.Contexts;
using FiveMForge.Models;
using FiveMForge.Utils;
using MySqlConnector;
using Player = CitizenFX.Core.Player;

namespace FiveMForge.Controller.Session
{
    /// <summary>
    /// Class <c>SessionController</c>
    /// Creates a unique session ID for every account.
    /// This session is used to validate API Access
    /// when playing on the server
    /// </summary>
    public class SessionController : BaseClass
    {
        public SessionController()
        {
            EventHandlers[ServerEvents.GetSessionId] += new Action<Player>(GetSessionId);
            EventHandlers[FiveMEvents.PlayerConnecting] += new Action<Player, string, dynamic, dynamic>(OnPlayerConnecting);
            EventHandlers[ServerEvents.Heartbeat] += new Action<Player>(OnHeartbeat);
            EventHandlers[FiveMEvents.PlayerDisconnecting] += new Action<Player>(ClearSessionId);
            EventHandlers[FiveMEvents.PlayerDisconnecting] += new Action<Player, string>(OnPlayerDisconnecting);
        }

        /// <summary>
        /// Handles the connection of a player to the server.
        /// Will check if the player is existing in our database, if he is it will update the login time,
        /// and send the client to the character selection screen. If we is a new player we check if the
        /// player is whitelisted. If he isn't whitelisted then we kick him and tell him to get whitelisted first.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="playerName"></param>
        /// <param name="setKickReason"></param>
        /// <param name="deferrals"></param>
        private async void OnPlayerConnecting([FromSource] Player player, string playerName, dynamic setKickReason,
            dynamic deferrals)
        {
            var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0); // Get first identifier
            Debug.WriteLine($"Player Joined: {playerIdentifier}");

            var currentPlayer = Context.Players.FirstOrDefault(p => p.AccountId == playerIdentifier);
            // If we have a player we don't have to continue, we only update the login time.
            if (currentPlayer != null)
            {
                currentPlayer.LastLogin = DateTime.Now.ToUniversalTime().ToString(CultureInfo.CurrentCulture);
                await Context.SaveChangesAsync();
                player.TriggerEvent(ServerEvents.GoToCharacterSelection);
                return;
            }

            // Create new Player Database Model
            var newPlayer = new Models.Player();
            // Every character has a UUID which is used to reference the account.
            // This way we can transfer user accounts to other Platforms: Steam <> Epic <> Rockstar
            newPlayer.Uuid = Guid.NewGuid().ToString();
            newPlayer.AccountId = playerIdentifier;
            newPlayer.LastLogin = DateTime.Now.ToUniversalTime().ToString(CultureInfo.CurrentCulture);
            Context.Players.Add(newPlayer);

            player.TriggerEvent(ServerEvents.GoToCharacterSelection);

            await Context.SaveChangesAsync();
        }

        private async void OnPlayerDisconnecting([FromSource] Player player, string reason)
        {
            // Get AccountID from FiveM
            var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);
            Debug.WriteLine($"Disconnecting: {playerIdentifier}");

            // Get Matching player in Database.
            var currentPlayer = Context.Players.FirstOrDefault(p => p.AccountId == playerIdentifier);
            if (currentPlayer == null)
            {
                return;
            }

            // Grab the last character position, if there's none we use 0:0:0
            var lastPosition = player.Character?.Position ?? Vector3.Zero;
            var character = Context.Characters.FirstOrDefault(c => c.Uuid == currentPlayer.Uuid);
            if (character == null)
            {
                return;
            }

            // Convert Position to our string format :)
            character.LastPos = $"{lastPosition.X}:{lastPosition.Y}:{lastPosition.Z}";
            await Context.SaveChangesAsync();
        }


        /// <summary>
        /// Returns an active session if the player asks for one.
        /// The premise is that the player is connected and whitelisted on our servers.
        /// </summary>
        /// <param name="player"></param>
        private async void GetSessionId([FromSource] Player player)
        {
            var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);

            var playerAccount = Context.Players.FirstOrDefault(p => p.AccountId == playerIdentifier);

            if (playerAccount == null) return;

            var playerSession = Context.Sessions.FirstOrDefault(s => s.AccountUuid == playerAccount.Uuid);
            
            if (playerSession != null)
            {
                // Should never reach here, means player has session when connecting.
                player.TriggerEvent(ServerEvents.DisconnectPlayer, new Error(ErrorTypes.NetworkError, 100));
                return;
            }

            var newSession = new Models.Session();
            newSession.AccountUuid = playerAccount.Uuid;
            newSession.SessionToken = Guid.NewGuid().ToString();
            newSession.ExpirationDate =
                DateTime.Now.AddMinutes(30).ToUniversalTime().ToString(CultureInfo.CurrentCulture);
            Context.Sessions.Add(newSession);
            await Context.SaveChangesAsync();
            player.TriggerEvent(ServerEvents.SessionIdLoaded, newSession.SessionToken);
        }

        /// <summary>
        /// Clears the sessionId from the database upon disconnecting.
        /// </summary>
        /// <param name="player"></param>
        private async void ClearSessionId([FromSource] Player player)
        {
            var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);
            var playerAccount = Context.Players.FirstOrDefault(p => p.AccountId == playerIdentifier);
            if (playerAccount == null) return;
            var sessionToDelete = Context.Sessions.FirstOrDefault(s => s.AccountUuid == playerAccount.Uuid);
            if (sessionToDelete != null)
            {
                Context.Sessions.Remove(sessionToDelete);
            }

            await Context.SaveChangesAsync();
        }
    
        /// <summary>
        /// Every client sends a heartbeat every 10 minutes, to
        /// keep the connection alive. If a client doesn't not send a heartbeat within
        /// that timeframe he is disconnected.
        /// </summary>
        /// <param name="player"></param>
        private async void OnHeartbeat([FromSource] Player player)
        {
            var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);
            var account = Context.Players.FirstOrDefault(p => p.AccountId == playerIdentifier);
            var session = Context.Sessions.FirstOrDefault(s => s.AccountUuid == account.Uuid);
            if (session == null)
            {
                // TODO: Disconnect player if session is not existing.
                return;
            }

            session.ExpirationDate = DateTime.Now.ToUniversalTime().ToString(CultureInfo.CurrentCulture);
            await Context.SaveChangesAsync();
        }
    }
}