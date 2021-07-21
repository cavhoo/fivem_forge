extern alias CFX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityOfMindClient.Models;
using Newtonsoft.Json;
using static CFX::CitizenFX.Core.Native.API;
using BaseScript = CFX::CitizenFX.Core.BaseScript;

namespace CityOfMindClient.Controller.Money
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
      var bankInfo = JsonConvert.DeserializeObject<BankInformation[]>(banks);
      foreach (var bank in bankInfo)
      {
        _bankLocations.Add(new(bank.Name, bank.SpriteId, bank.X, bank.Y, bank.Z, bank.IsActive, bank.IsAdminOnly));
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

      Tick += RenderBankMarkers;
    }

    private async Task RenderBankMarkers()
    {
      await Delay(16); // 16ms = 1 frame @ 60 fps
      foreach (var bankLocation in _bankLocations)
      {
        DrawMarker(
          1,
          bankLocation.X,
          bankLocation.Y,
          bankLocation.Z,
          0f,
          0f,
          0f,
          0f,
          0f,
          0f,
          1.0f,
          1.0f,
          2.0f,
          255,
          255,
          255,
          255,
          false,
          false,
          2,
          false,
          null,
          null,
          false
        );
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