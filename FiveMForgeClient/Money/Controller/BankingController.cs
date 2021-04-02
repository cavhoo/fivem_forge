using System;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using FiveMForgeClient.Models;
using static CitizenFX.Core.Native.API;

namespace FiveMForgeClient.ATM.Controller
{
    public class BankingController : BaseClass
    {
        private const int MinimumDistance = 3;
        private Vector3[] _bankLocations;
        protected override void OnClientResourceStart(string resourceName)
        {
            EventHandlers[ServerEvents.BankLocationsLoaded] += new Action<Vector3[]>(OnBankLocationsLoaded);
            EventHandlers["playerSpawned"] += new Action(OnPlayerSpawned);
        }

        private void OnPlayerSpawned()
        {
            TriggerServerEvent(ServerEvents.LoadBankLocations);
        }

        private void OnBankLocationsLoaded(Vector3[] obj)
        {
            _bankLocations = obj;
            TriggerEvent("chat:addMessage", new
            {
                color = new[] {125, 255, 255},
                args = new[] {$"Received Bank locations: {obj.Length}"}
            });
            Tick += RenderAtmBlips;
            Tick += HandleNearBank;
        }


        private async Task RenderAtmBlips()
        {
            await Delay(250);
            foreach (var bankLocation in _bankLocations)
            {
                var blip = AddBlipForCoord(bankLocation.X, bankLocation.Y, bankLocation.Z);
                SetBlipSprite(blip, 108); // 108 is the Bank Sprite ID in GTA5
                SetBlipScale(blip, 0.7f);
                SetBlipAsShortRange(blip, true);
                BeginTextCommandSetBlipName("STRING");
                AddTextComponentString("ATM");
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
                GetDistanceBetweenCoords(b.X, b.Y, b.Z, pLocation.X, pLocation.Y, pLocation.Z, false) <= MinimumDistance);

            return bankInProximity.Any();
        }
    }
}