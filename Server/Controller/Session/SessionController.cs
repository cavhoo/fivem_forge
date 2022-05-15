using System;
using System.Globalization;
using System.Linq;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Server.Controller.Base;
using Server.Models;
using Server.Models.Enums;
using Player = CitizenFX.Core.Player;

namespace Server.Controller.Session
{
    /// <summary>
    /// Class <c>SessionController</c>
    /// Creates a unique session ID for every account.
    /// This session is used to validate API Access
    /// when playing on the server
    /// </summary>
    public class SessionController : BaseClass
    {
        public SessionController(EventHandlerDictionary handlers, Action<string, object[]> eventTriggerFunc,
                                       Action<Player, string, object[]> clientEventTriggerFunc, Action<string, object[]> clientEventTriggerAllFunc) : base(handlers, eventTriggerFunc, clientEventTriggerFunc, clientEventTriggerAllFunc)
        {
            EventHandlers[ClientEvents.GetSessionId] += new Action<Player>(GetSessionId);
            //EventHandlers[FiveMEvents.PlayerConnecting] += new Action<Player, string, dynamic, dynamic>(OnPlayerConnecting);
            EventHandlers[ServerEvents.Heartbeat] += new Action<Player>(OnHeartbeat);
            EventHandlers[FiveMEvents.PlayerDisconnecting] += new Action<Player>(ClearSessionId);
            Debug.WriteLine("Started SessionController");
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
                //Debug.WriteLine("Spawning player");
                //player.TriggerEvent("CityOfMind:SpawnPlayer");
                return;
            }

            // Create new Player Database Model
            var newPlayer = new Models.Player();
            // Every character has a UUID which is used to reference the account.
            // This way we can transfer user accounts to other Platforms: Steam <> Epic <> Rockstar
            newPlayer.AccountUuid = Guid.NewGuid().ToString();
            newPlayer.AccountId = playerIdentifier;
            newPlayer.LastLogin = DateTime.Now.ToUniversalTime().ToString(CultureInfo.CurrentCulture);
            newPlayer.Tier = PlayerTier.COMMON;
            Context.Players.Add(newPlayer);
            
            
            // TODO: Remove this once we have a character creation tool.
            var newCharacter = new Models.Character.Character();
            newCharacter.AccountUuid = newPlayer.AccountUuid;
            newCharacter.CharacterUuid = Guid.NewGuid().ToString();
            newCharacter.InUse = true;
            newCharacter.LastPos = "-1046.6901:-2770.3647:4.62854"; // Temp Airport coodinate
            Context.Characters.Add(newCharacter);
            
            
            //player.TriggerEvent("CityOfMind:SpawnPlayer");
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
            Debug.WriteLine($"Saving last position: {lastPosition.ToString()}");
            var character = Context.Characters.FirstOrDefault(c => c.AccountUuid == currentPlayer.AccountUuid);
            if (character == null)
            {
                var newCharacter = new Models.Character.Character()
                {
                    AccountUuid = currentPlayer.AccountUuid,
                    CharacterUuid = Guid.NewGuid().ToString(),
                    InUse = true,
                    LastPos = $"{lastPosition.X}:{lastPosition.Y}:{lastPosition.Z}",
                    JobUuid = null
                };
                Context.Characters.Add(newCharacter);
                await Context.SaveChangesAsync();
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

            var playerSession = Context.Sessions.FirstOrDefault(s => s.AccountUuid == playerAccount.AccountUuid);
            
            if (playerSession != null)
            {
                // Should never reach here, means player has session when connecting.
                player.TriggerEvent(ServerEvents.DisconnectPlayer, new Error(ErrorTypes.NetworkError, 100));
                return;
            }

            var newSession = new Models.Session();
            newSession.AccountUuid = playerAccount.AccountUuid;
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
            var sessionToDelete = Context.Sessions.FirstOrDefault(s => s.AccountUuid == playerAccount.AccountUuid);
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
            var session = Context.Sessions.FirstOrDefault(s => s.AccountUuid == account.AccountUuid);
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
