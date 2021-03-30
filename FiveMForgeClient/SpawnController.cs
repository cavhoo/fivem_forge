using System;
using static CitizenFX.Core.Native.API;

namespace FiveMForgeClient
{
    public class SpawnController : BaseClass
    {
        protected override void OnClientResourceStart(string resourceName)
        {
            EventHandlers["playerSpawned"] += new Action(OnPlayerSpawned);
            EventHandlers["FiveMForge:SpawnAt"] += new Action<float, float, float>(OnUpdateSpawnPosition);
        }

        private void OnPlayerSpawned()
        {
            // Get last saved player from server
            TriggerServerEvent("FiveMForge:GetLastSpawnPosition");
        }

        private void OnUpdateSpawnPosition(float x, float y, float z)
        {
            var pedId = PlayerPedId();
            SetEntityCoords(pedId, x, y, z + 0.001f, false, false, false, true);
        }
    }
}