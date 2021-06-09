extern alias CFX;

using System;
using CFX::CitizenFX.Core;
using FiveMForgeClient.Models;
using static CFX::CitizenFX.Core.Native.API;

namespace FiveMForgeClient.Controller
{
    public class SpawnController : BaseScript
    {
        private bool Instantiated { get; set; }
        public SpawnController()
        {
            EventHandlers[ClientEvents.ScriptStart] += new Action<string>(OnClientResourceStart);
        }
        private void OnClientResourceStart(string resourceName)
        {
            if (Instantiated) return;
            Instantiated = true;
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
