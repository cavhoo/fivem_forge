using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using fastJSON;
using FiveMForgeClient.Models;
using static CitizenFX.Core.Native.API;

namespace FiveMForgeClient.ATM.Controller
{
    public class AtmController : BaseScript
    {
        private bool Instantiated { get; set; }
        private const int MiniumDistance = 3;
        private List<Vector3> _atmLocations = new List<Vector3>();

        public AtmController()
        {
            EventHandlers[ClientEvents.ScriptStart] += new Action<string>(OnClientResourceStart);
        }

        private void OnClientResourceStart(string resourceName)
        {
            if (Instantiated) return;
            Instantiated = true;
            EventHandlers[ServerEvents.AtmLocationsLoaded] += new Action<string>(OnAtmLocationsRetrieved);
            TriggerServerEvent(ServerEvents.LoadAtmLocations);
        }

        private void OnAtmLocationsRetrieved(string atms)
        {
            var atmObject = JSON.Parse(atms);
            if (!(atmObject is IEnumerable atmArray)) return;
            foreach (Dictionary<string, object> atm in atmArray)
            {
                _atmLocations.Add(new Vector3(Convert.ToSingle(atm["X"]), Convert.ToSingle(atm["Y"]), Convert.ToSingle(atm["Z"])));
            }
            Tick += HandleNearAtm;
        }

        private async Task HandleNearAtm()
        {
            await Delay(250);
            if (IsNearAtm())
            {
                // Display text for interaction.
                DisplayHelpTextThisFrame($"You are close to an ATM. Maybe you want to withdraw/deposit", true);
            }
        }

        private bool IsNearAtm()
        {
            var playerLocation = GetEntityCoords(PlayerPedId(), false);
            var atmsInProximity = _atmLocations.Where(p =>
                GetDistanceBetweenCoords(p.X, p.Y, p.Z, playerLocation.X, playerLocation.Y, playerLocation.Z, true) <=
                MiniumDistance);

            return atmsInProximity.Any();
        }
    }
}