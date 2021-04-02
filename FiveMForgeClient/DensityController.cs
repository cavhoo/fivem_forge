using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace FiveMForgeClient
{
    public class DensityController : BaseClass
    {
        // Density for Vehicles and Pedestrians.
        // 0 = none, 1.0f = full
        private readonly float DENSITY_MULTIPLIER = 1.0f;

        protected override void OnClientResourceStart(string resourceName)
        {
            Tick += RunSetDensityTick;
        }

        /**
         * Runs every game tick to set the density for the player.
         */
        private async Task RunSetDensityTick()
        {
            await Delay(500);
            SetParkedVehicleDensityMultiplierThisFrame(DENSITY_MULTIPLIER);
            SetPedDensityMultiplierThisFrame(DENSITY_MULTIPLIER);
            SetRandomVehicleDensityMultiplierThisFrame(DENSITY_MULTIPLIER);
            SetVehicleDensityMultiplierThisFrame(DENSITY_MULTIPLIER);
            SetScenarioPedDensityMultiplierThisFrame(DENSITY_MULTIPLIER, DENSITY_MULTIPLIER);
        }
    }
}