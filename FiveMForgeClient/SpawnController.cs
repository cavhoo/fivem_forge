using System;
using FiveMForgeClient.Models;
using static CitizenFX.Core.Native.API;

namespace FiveMForgeClient
{
    public class SpawnController : BaseClass
    {
        protected override void OnClientResourceStart(string resourceName)
        {
            EventHandlers["playerSpawned"] += new Action(OnPlayerSpawned);
            EventHandlers[ServerEvents.SpawnAt] += new Action<float, float, float>(OnUpdateSpawnPosition);
        }

        private void OnPlayerSpawned()
        {
            // Get last saved player position from server
            TriggerServerEvent(ServerEvents.GetLastSpawnPosition);
        }

        private void OnUpdateSpawnPosition(float x, float y, float z)
        {
            var pedId = PlayerPedId();
            SetEntityCoords(pedId, x, y, z + 0.001f, false, false, false, true);
        }
    }
}