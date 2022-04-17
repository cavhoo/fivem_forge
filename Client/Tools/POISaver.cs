extern alias CFX;
using System;
using System.Collections.Generic;
using Client.Models;
using static CFX::CitizenFX.Core.Native.API;
using BaseScript = CFX::CitizenFX.Core.BaseScript;

namespace Client.Tools
{
    public class POISaver : BaseScript
    {
        public POISaver()
        {
            EventHandlers[ClientEvents.ScriptStart] += new Action<string>(OnClientStart);
        }

        private void OnClientStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;
            RegisterCommand("savepoi", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                var poiType = "Unknown";
                if (args.Count > 0)
                {
                    poiType = args[0].ToString();
                }

                TriggerServerEvent(ServerEvents.SavePOIPosition, poiType);
            }), false);
        }
    }
}