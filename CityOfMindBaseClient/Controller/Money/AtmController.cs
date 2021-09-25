extern alias CFX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CFX::CitizenFX.Core;
using CityOfMindClient.Models;
using Newtonsoft.Json;
using static CFX::CitizenFX.Core.Native.API;
using BaseScript = CFX::CitizenFX.Core.BaseScript;
using Vector3 = CFX::CitizenFX.Core.Vector3;

namespace CityOfMindClient.Controller.Money
{
  public class BankAccountInformation
  {
    public int Saldo;
    public int[] WithdrawableAmounts;
    public string AccountNumber;
    public string AccountHolder;
  }

  public class AtmController : BaseScript
  {
    private bool Instantiated { get; set; }
    private const float MiniumDistance = 1.0f;
    private List<Vector3> _atmLocations = new List<Vector3>();

    public AtmController()
    {
      EventHandlers[ServerEvents.AtmLocationsLoaded] += new Action<string>(OnAtmLocationsRetrieved);
      EventHandlers[ServerEvents.BankAccountLoaded] += new Action<string>(OnBankAccountLoaded);
      TriggerServerEvent(ServerEvents.LoadAtmLocations);
      RegisterNuiCallbackType("atmMachine");
      EventHandlers["__cfx_nui:atmMachine"] += new Action<IDictionary<string, dynamic>, CallbackDelegate>(HandleNuiCallbacks);
    }

    private void HandleNuiCallbacks(IDictionary<string, dynamic> payload, CallbackDelegate cb)
    {
      if (payload.ContainsKey("getAccount"))
      {
          TriggerServerEvent(ServerEvents.LoadBankAccount);
      }

      if (payload.ContainsKey("withdraw"))
      {
        dynamic withDrawAmount = 0;
        payload.TryGetValue("withdraw", out withDrawAmount);
        if (withDrawAmount == null)
        {
          cb(new
          {
            status = "Could not withdraw money",
          });
          return;
        }
        TriggerServerEvent(ServerEvents.WithdrawMoney, (int) withDrawAmount);
      }

      if (payload.ContainsKey("deposit"))
      {
        dynamic depositMoney = 0;
        payload.TryGetValue("deposit", out depositMoney);
        if (depositMoney == null)
        {
          cb(new
          {
            status = "Could not deposit money"
          });
          return;
        }
      }

      if (payload.ContainsKey("close"))
      {
      SetNuiFocus(false, false);
      SendNuiMessage(JsonConvert.SerializeObject(
        new
        {
          targetUI = "atmMachine",
          payload = new
          {
            eventType = "close",
          },
        }));
      }

      cb(new { status = "success"});
    }

    private void OnAtmLocationsRetrieved(string atms)
    {
      var atmObject = JsonConvert.DeserializeObject<string[]>(atms);
      if (atmObject == null) return;
      foreach (string atm in atmObject)
      {
        // Locations are encoded with double " so we need to trim the inner ' before parsing it.
        var location = atm.Trim('\'').Split(':');
        _atmLocations.Add(new Vector3(Convert.ToSingle(location[0]), Convert.ToSingle(location[1]),
          Convert.ToSingle(location[2])));
      }

      Debug.WriteLine(_atmLocations.ToString());

      Tick += HandleNearAtm;
      Tick += DrawAtmMarkers;
    }

    private async Task HandleNearAtm()
    {
      await Delay(16); // 16ms = 1 Frame @ 60 fps
      if (IsNearAtm())
      {
        if (Game.IsControlJustPressed(0, Control.Arrest))
        {
          SendNuiMessage(JsonConvert.SerializeObject(new
          {
            targetUI = "atmMachine",
            payload = new
            {
              eventType = "open",
            },
          }));
          SetNuiFocus(true, true);
        }
      }
    }

    private void OnBankAccountLoaded(string account)
    {
      var bankAccountInformation = JsonConvert.DeserializeObject<BankAccountInformation>(account);
      
      SendNuiMessage(JsonConvert.SerializeObject(new
      {
        targetUI = "atmMachine",
        payload = new
        {
          eventType = "accountLoaded",
          saldo = bankAccountInformation.Saldo,
          accountOwner = bankAccountInformation.AccountHolder,
          withdrawableAmounts = bankAccountInformation.WithdrawableAmounts,
          accountNumber = bankAccountInformation.AccountNumber
        }
      }));
    }

    private async Task DrawAtmMarkers()
    {
      foreach (var atmLocation in _atmLocations)
      {
        DrawMarker(1, atmLocation.X, atmLocation.Y, atmLocation.Z - 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f,
          1.0f, 2.0f, 255, 255, 255, 80, false, false, 2, false, null, null, false);
      }
    }

    private bool IsNearAtm()
    {
      var playerLocation = GetEntityCoords(PlayerPedId(), false);
      var atmsInProximity = _atmLocations.Where(p =>
        GetDistanceBetweenCoords(p.X, p.Y, p.Z, playerLocation.X, playerLocation.Y, playerLocation.Z, true) <=
        MiniumDistance);
      return atmsInProximity.Any();
    }
  }
}