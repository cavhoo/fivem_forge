extern alias CFX;
using System;
using System.Threading.Tasks;
using CFX::CitizenFX.Core;
using FiveMForgeClient.Models;
using static CFX::CitizenFX.Core.Native.API;

namespace FiveMForgeClient.Controller
{
  public class SpawnController : BaseScript
  {
    private bool ForceRespawn = true;
    private int TimeOfDeath = -1;
    private bool Instantiated { get; set; }
    private bool SpawnLock { get; set; }

    public SpawnController()
    {
      //EventHandlers[ClientEvents.ScriptStart] += new Action<string>(OnClientResourceStart);
    }

    private void OnClientResourceStart(string resourceName)
    {
      if (Instantiated) return;
      Instantiated = true;
      EventHandlers[ServerEvents.SpawnAt] += new Action<float, float, float>(SpawnPlayer);
      Tick += SpawnLoop;
    }


    private async Task SpawnLoop()
    {
      await Delay(50); // 50 ms Polling rate
      var playerPed = PlayerPedId();
      if (playerPed != null && playerPed != -1)
      {
        if (NetworkIsPlayerActive(PlayerId()))
        {
          if (ForceRespawn || TimeOfDeath > 0 && (Math.Abs(GetTimeDifference(GetGameTimer(), TimeOfDeath))) > 2000)
          {
            // Load last spawn position
            // TODO: Append selected character UUID to grab the correct spawn point ;) 
            TriggerServerEvent(ServerEvents.GetLastSpawnPosition);
            ForceRespawn = false;
          }
        }

        if (IsEntityDead(playerPed) && TimeOfDeath < 0)
        {
          TimeOfDeath = GetGameTimer();
        }
        else
        {
          TimeOfDeath = -1;
        }
      }
    }

    private void FreezePlayer(int id, bool freeze)
    {
      var player = id;
      SetPlayerControl(player, !freeze, 1 << 2);

      var ped = GetPlayerPed(player);

      if (!freeze)
      {
        if (!IsEntityVisible(ped))
        {
          SetEntityVisible(ped, true, false);
        }

        if (!IsPedInAnyVehicle(ped, false))
        {
          SetEntityCollision(ped, true, true);
        }

        FreezeEntityPosition(ped, false);
        SetPlayerInvincible(id, false);
      }
      else
      {
        if (IsEntityVisible(ped))
        {
          SetEntityVisible(ped, false, false);
        }

        SetEntityCollision(ped, false, true);
        FreezeEntityPosition(ped, true);
        SetPlayerInvincible(id, true);
        if (!IsPedFatallyInjured(ped))
        {
          ClearPedTasksImmediately(ped);
        }
      }
    }

    private async void SpawnPlayer(float x, float y, float z)
    {
      if (SpawnLock) return;

      SpawnLock = true;

      DoScreenFadeOut(500);
      while (!IsScreenFadedOut())
      {
        await Delay(5);
      }

      FreezePlayer(PlayerId(), true);
      var modelHashKey = GetHashKey("a_m_y_hipster_01");
      RequestModel((uint) modelHashKey);

      while (!HasModelLoaded((uint) modelHashKey))
      {
        RequestModel((uint) modelHashKey);
        Debug.WriteLine("Wating for model to load...");
        await Delay(5);
      }

      SetPlayerModel(PlayerId(), (uint) modelHashKey);
      SetModelAsNoLongerNeeded((uint) modelHashKey);

      var ped = PlayerPedId();
      RequestCollisionAtCoord(x, y, z);
      SetEntityCoordsNoOffset(ped, x, y, z, true, false, false);
      NetworkResurrectLocalPlayer(x, y, z, 0, true, false);
      ClearPedTasksImmediately(ped);
      RemoveAllPedWeapons(ped, true);
      ClearPlayerWantedLevel(PlayerId());


      var time = GetGameTimer();
      while (!HasCollisionLoadedAroundEntity(ped) && (GetGameTimer() - time) < 5000)
      {
        await Delay(0);
      }

      ShutdownLoadingScreen();

      if (IsScreenFadedOut())
      {
        DoScreenFadeIn(500);
        while (!IsScreenFadedIn())
        {
          await Delay(5);
        }
      }

      FreezePlayer(PlayerId(), false);
      SpawnLock = false;
    }

    private void OnPlayerSpawned()
    {
      // Get last saved player position from server
      TriggerServerEvent(ServerEvents.GetLastSpawnPosition);
    }

    private async void OnShowCharacterCreation(float x, float y, float z)
    {
      if (SpawnLock) return;
      SpawnLock = true;

      DoScreenFadeOut(500);
      while (!IsScreenFadedOut())
      {
        await Delay(5);
      }

      FreezePlayer(PlayerId(), true);
      var modelHashKey = GetHashKey("a_m_y_hipster_01");
      RequestModel((uint) modelHashKey);

      while (!HasModelLoaded((uint) modelHashKey))
      {
        RequestModel((uint) modelHashKey);
        await Delay(5);
      }

      SetPlayerModel(PlayerId(), (uint) modelHashKey);
      SetModelAsNoLongerNeeded((uint) modelHashKey);

      var ped = PlayerPedId();
      RequestCollisionAtCoord(x, y, z);
      SetEntityCoordsNoOffset(ped, x, y, z, true, false, false);
      NetworkResurrectLocalPlayer(x, y, z, 0, true, false);
      ClearPedTasksImmediately(ped);
      RemoveAllPedWeapons(ped, true);
      ClearPlayerWantedLevel(PlayerId());

      ShutdownLoadingScreen();

      if (IsScreenFadedOut())
      {
        DoScreenFadeIn(500);
        while (!IsScreenFadedIn())
        {
          await Delay(5);
        }
      }
    }
  }
}