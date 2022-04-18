using System;
using System.Collections.Generic;
using Client.Models;
using static CitizenFX.Core.Native.API;
using BaseScript = CitizenFX.Core.BaseScript;
using Game = CitizenFX.Core.Game;
using VehicleSeat = CitizenFX.Core.VehicleSeat;
using World = CitizenFX.Core.World;

namespace Client.Tools
{
    public class CarSpawner : BaseScript
    {
        public CarSpawner()
        {
            EventHandlers[ClientEvents.ScriptStart] += new Action<string>(OnClientResourceStart);
        }
        private void OnClientResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;
            RegisterCommand("car", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                // Default Model
                var model = "adder";

                if (args.Count > 0)
                {
                    model = args[0].ToString();
                }

                var hash = (uint) GetHashKey(model);
                if (!IsModelInCdimage(hash) || !IsModelAVehicle(hash))
                {
                    TriggerEvent("chat:addMessage", new
                    {
                        color = new[] {255, 0, 0},
                        args = new[] {"[CarSpawner] That didn't work"}
                    });
                    return;
                }

                var vehicle = await World.CreateVehicle(model, Game.PlayerPed.Position, Game.PlayerPed.Heading);

                Game.PlayerPed.SetIntoVehicle(vehicle, VehicleSeat.Driver);

                TriggerEvent("chat:addMessage", new
                {
                    color = new[] {255, 0, 255},
                    args = new[] {"[CarSpawner] Enjoy your wheels!"}
                });
            }), false);
        }
    }
}