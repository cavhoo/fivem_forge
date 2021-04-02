using System;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using FiveMForgeClient.Models;
using static CitizenFX.Core.Native.API;

namespace FiveMForgeClient.ATM.Controller
{
    public class AtmController : BaseClass
    {
        private const int MiniumDistance = 3;
        private Vector3[] _atmLocations;

        protected override void OnClientResourceStart(string resourceName)
        {
            EventHandlers[ServerEvents.AtmLocationsLoaded] += new Action<Vector3[]>(OnAtmLocationsRetrieved);

            TriggerServerEvent(ServerEvents.LoadAtmLocations);
        }

        private void OnAtmLocationsRetrieved(Vector3[] atms)
        {
            _atmLocations = atms;
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