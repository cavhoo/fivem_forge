using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using fastJSON;
using FiveMForgeClient.Models;
using static CitizenFX.Core.Native.API;

namespace FiveMForgeClient.Controller
{
    public class BankingController : BaseScript
    {
        private bool Instantiated { get; set; }
        private const int MinimumDistance = 3;
        private bool _spawned = false;
        private List<BankInformation> _bankLocations = new List<BankInformation>();

        public BankingController()
        {
            EventHandlers[ClientEvents.ScriptStart] += new Action<string>(OnClientResourceStart);
        }

        private void OnClientResourceStart(string resourceName)
        {
            if (Instantiated) return;
            Instantiated = true;
            EventHandlers[ServerEvents.BankLocationsLoaded] +=
                new Action<string>(OnBankLocationsLoaded);
            EventHandlers["playerSpawned"] += new Action(OnPlayerSpawned);
        }

        private void OnPlayerSpawned()
        {
            if (_spawned) return;
            _spawned = true;
            if (_bankLocations.Count != 0) return;
            TriggerServerEvent(ServerEvents.LoadBankLocations);
        }

        private void OnBankLocationsLoaded(string banks)
        {
            var bankInfo = JSON.Parse(banks);
            if (!(bankInfo is IEnumerable bankArray)) return;
            foreach (Dictionary<string, object> bank in bankArray)
            {
                var name = Convert.ToString(bank["Name"]);
                var spriteID = Convert.ToInt32(bank["SpriteId"]);
                var x = Convert.ToSingle(bank["X"]);
                var y = Convert.ToSingle(bank["Y"]);
                var z = Convert.ToSingle(bank["Z"]);
                var isActive = Convert.ToBoolean(bank["IsActive"]);
                var isAdminOnly = Convert.ToBoolean(bank["IsAdminOnly"]);
                _bankLocations.Add(new (name, spriteID, x, y, z, isActive, isAdminOnly));
            }
            RenderBankBlips();
        }


        private void RenderBankBlips()
        {
            foreach (var bankLocation in _bankLocations)
            {
                var blip = AddBlipForCoord(bankLocation.X, bankLocation.Y, bankLocation.Z);
                SetBlipSprite(blip, 108); // 108 is the Bank Sprite ID in GTA5
                SetBlipScale(blip, 0.7f);
                SetBlipAsShortRange(blip, true);
                BeginTextCommandSetBlipName("STRING");
                AddTextComponentString("Bank");
                EndTextCommandSetBlipName(blip);
            }
        }

        private async Task HandleNearBank()
        {
            await Delay(250);
            if (IsNearBank())
            {
                // Display Information that near bank.
                DisplayHelpTextThisFrame($"You are close to a bank. Maybe you want to withdraw/deposit.", true);
            }
        }

        private bool IsNearBank()
        {
            var pLocation = GetEntityCoords(PlayerPedId(), true);
            var bankInProximity = _bankLocations.Where(b =>
                GetDistanceBetweenCoords(b.X, b.Y, b.Z, pLocation.X, pLocation.Y, pLocation.Z, false) <=
                MinimumDistance);

            return bankInProximity.Any();
        }
    }
}