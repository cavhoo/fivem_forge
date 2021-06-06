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
    /// when playing on the server.
    /// </summary>
    public class SessionController : BaseClass
    {
        public SessionController()
        {
            EventHandlers[ServerEvents.GetSessionId] += new Action<Player>(GetSessionId);
            EventHandlers[ServerEvents.Heartbeat] += new Action<Player>(OnHeartbeat);
            EventHandlers["playerDropped"] += new Action<Player>(ClearSessionId);
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
                return;
            }

            var newSession = new Models.Session();
            newSession.AccountUuid = playerAccount.Uuid;
            newSession.SessionToken= Guid.NewGuid().ToString();
            newSession.ExpirationDate = DateTime.Now.AddMinutes(30).ToUniversalTime().ToString(CultureInfo.CurrentCulture);
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