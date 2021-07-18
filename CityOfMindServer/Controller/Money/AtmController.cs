using System;
using System.Collections.Generic;
using System.Linq;
using CitizenFX.Core;
using FiveMForge.Controller.Base;
using FiveMForge.Database;
using FiveMForge.Database.Contexts;
using FiveMForge.Models;
using FiveMForge.Utils;
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
            Debug.WriteLine("Started ATM Controller");
            EventHandlers[ServerEvents.LoadAtmLocations] += new Action<Player>(OnAtmLocationsRequested);
        }

        private void OnAtmLocationsRequested([FromSource] Player player)
        {
            var atmLocations = Context.Atms.Select(a => a.Location);
            var parsedLocations = new List<Vector3>();
            foreach (var atmLocation in atmLocations)
            {
                var split = atmLocation.Split(':');
                //parsedLocations.Add(Converter.PositionStringToVector3(atmLocation));
            }

            TriggerClientEvent(player, ServerEvents.AtmLocationsLoaded, JsonConvert.SerializeObject(parsedLocations.ToArray()));
        }
    }
}