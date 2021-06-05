using System;
using System.Linq;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FiveMForge.Controller.Base;
using FiveMForge.Database;
using FiveMForge.Database.Contexts;
using FiveMForge.Utils;
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
            using var ctx = new CoreContext();
            var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);

            var playerAccount = ctx.Players.FirstOrDefault(p => p.AccountId == playerIdentifier);

            if (playerAccount == null) return;
            
            var playerSession = ctx.Sessions.FirstOrDefault(s => s.AccountUuid == playerAccount.Uuid);
            if (playerSession != null)
            {
                // Should never reach here, means player has session when connecting.
                return;
            }

            var newSession = new Database.Models.Session();
            newSession.AccountUuid = playerAccount.Uuid;
            newSession.SessionToken= Guid.NewGuid().ToString();
            ctx.Sessions.Add(newSession);
            await ctx.SaveChangesAsync();
        }

        private async void ClearSessionId([FromSource] Player player)
        {
            using var ctx = new CoreContext();
            
            var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);
            var playerAccount = ctx.Players.FirstOrDefault(p => p.AccountId == playerIdentifier);
            if (playerAccount != null)
            {
                var sessionToDelete = ctx.Sessions.FirstOrDefault(s => s.AccountUuid == playerAccount.Uuid);
                if (sessionToDelete != null)
                {
                    ctx.Sessions.Remove(sessionToDelete);
                }
            }
        }
    }
}