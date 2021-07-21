extern alias CFX;
using System;
using System.Threading.Tasks;
using CityOfMindClient.Models;
using static CFX::CitizenFX.Core.Native.API;
using BaseScript = CFX::CitizenFX.Core.BaseScript;

namespace CityOfMindClient.Controller.Environment
{

    public class DensityController : BaseScript
    {
        // Density for Vehicles and Pedestrians.
        // 0 = none, 1.0f = full
        private readonly float DENSITY_MULTIPLIER = 1.0f;
        private bool Instantiated { get; set; }

        public DensityController()
        {
            EventHandlers[ClientEvents.ScriptStart] += new Action<string>(OnClientResourceStart);
        }
        
        private void OnClientResourceStart(string resourceName)
        {
            if (Instantiated) return;
            Instantiated = true;
            Tick += RunSetDensityTick;
        }

        /**
         * Runs every game tick to set the density for the player.
         */
        private async Task RunSetDensityTick()
        {
            await Delay(16);
            SetParkedVehicleDensityMultiplierThisFrame(DENSITY_MULTIPLIER);
            SetPedDensityMultiplierThisFrame(DENSITY_MULTIPLIER);
            SetRandomVehicleDensityMultiplierThisFrame(DENSITY_MULTIPLIER);
            SetVehicleDensityMultiplierThisFrame(DENSITY_MULTIPLIER);
            SetScenarioPedDensityMultiplierThisFrame(DENSITY_MULTIPLIER, DENSITY_MULTIPLIER);
        }
    }
}