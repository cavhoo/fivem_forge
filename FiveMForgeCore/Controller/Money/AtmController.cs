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
using Player = CitizenFX.Core.Player;

namespace FiveMForge.Controller.Money
{
    /// <summary>
    /// Class <c>AtmController</c>
    /// Controls the commands available at ATMs, from
    /// withdrawing money to making deposits. Also send
    /// the information about each atm location to the
    /// client.
    /// </summary>
    public class AtmController : BaseClass
    {
        public AtmController()
        {
            EventHandlers[ServerEvents.LoadAtmLocations] += new Action<Player, string>(OnAtmLocationsRequested);
        }

        private void OnAtmLocationsRequested([FromSource] Player player, string sessionId)
        {
            var atmLocations = Context.Atms.Select(a => Converter.PositionStringToVector3(a.Location)).ToList();
            TriggerClientEvent(player, ServerEvents.AtmLocationsLoaded, JsonConvert.SerializeObject(atmLocations));
        }
    }
}