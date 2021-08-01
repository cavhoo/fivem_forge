extern alias CFX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityOfMindClient.Models;
using Newtonsoft.Json;
using static CFX::CitizenFX.Core.Native.API;
using BaseScript = CFX::CitizenFX.Core.BaseScript;
using Vector3 = CFX::CitizenFX.Core.Vector3;

namespace CityOfMindClient.Controller.Money
{
    public class AtmController : BaseScript
    {
        private bool Instantiated { get; set; }
        private const int MiniumDistance = 3;
        private List<Vector3> _atmLocations = new List<Vector3>();

        public AtmController()
        {
            EventHandlers[ServerEvents.AtmLocationsLoaded] += new Action<string>(OnAtmLocationsRetrieved);
            TriggerServerEvent(ServerEvents.LoadAtmLocations);
        }

        private void OnAtmLocationsRetrieved(string atms)
        {
            var atmObject = JsonConvert.DeserializeObject<Vector3[]>(atms);
            if (!(atmObject is IEnumerable atmArray)) return;
            foreach (Dictionary<string, object> atm in atmArray)
            {
                _atmLocations.Add(new Vector3(Convert.ToSingle(atm["X"]), Convert.ToSingle(atm["Y"]),
                    Convert.ToSingle(atm["Z"])));
            }

            Tick += HandleNearAtm;
            Tick += DrawAtmMarkers;
        }

        private async Task HandleNearAtm()
        {
            await Delay(16); // 16ms = 1 Frame @ 60 fps
            if (IsNearAtm())
            {
                // Display text for interaction.
                DisplayHelpTextThisFrame($"You are close to an ATM. Maybe you want to withdraw/deposit", true);
            }
        }

        private async Task DrawAtmMarkers()
        {
            foreach (var atmLocation in _atmLocations)
            {
                DrawMarker(1, atmLocation.X, atmLocation.Y, atmLocation.Z - 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f,
                    1.0f, 2.0f, 255, 255, 255, 255, false, false, 2, false, null, null, false);
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
