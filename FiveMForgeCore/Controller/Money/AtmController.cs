using System;
using System.Collections.Generic;
using System.Linq;
using CitizenFX.Core;
using FiveMForge.Controller.Base;
using FiveMForge.Database;
using FiveMForge.Database.Contexts;
using FiveMForge.Models;
using FiveMForge.Utils;
using MySqlConnector;
using Newtonsoft.Json;

namespace FiveMForge.Controller.Money
{
    public class AtmController : BaseClass
    {
        public AtmController()
        {
            EventHandlers[ServerEvents.LoadAtmLocations] += new Action<Player, string>(OnAtmLocationsRequested);
        }

        private async void OnAtmLocationsRequested([FromSource] Player player, string sessionId)
        {
            using var ctx = new CoreContext();
            var atmLocations = ctx.Atms.Select(a => Converter.PositionStringToVector3(a.Location)).ToList();
            TriggerClientEvent(player, ServerEvents.AtmLocationsLoaded, JsonConvert.SerializeObject(atmLocations));
        }
    }
}