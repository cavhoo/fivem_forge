extern alias CFX;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CityOfMindClient.Enums;
using CityOfMindClient.Models;
using FiveMForgeClient.Utils;
using static CFX::CitizenFX.Core.Native.API;
using BaseScript = CFX::CitizenFX.Core.BaseScript;
using Math = System.Math;
using Vector3 = CFX::CitizenFX.Core.Vector3;

namespace CityOfMindClient.Controller.Spawn
{
  public class SpawnController : BaseScript
  {
    private bool ForceRespawn = true;
    private int TimeOfDeath = -1;
    private bool Instantiated { get; set; }
    private bool SpawnLock { get; set; }

    public SpawnController()
    {
      EventHandlers[ClientEvents.ScriptStart] += new Action<string>(OnClientResourceStart);
    }

    private void OnClientResourceStart(string resourceName)
    {
      if (Instantiated) return;
      Instantiated = true;
      EventHandlers[ClientEvents.SpawnPlayer] += new Action<dynamic, int>(SpawnPlayer);
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

    private async void SpawnPlayer(dynamic character, int cameraHandle)
    {
      var characterToSpawn = character as IDictionary<string, object>;
      // Parsing X:Y:Z to Vector 3
      var lastPos = Parser.StringToVector3((string)characterToSpawn?["LastPos"]);
      if (SpawnLock) return;
      SpawnLock = true;
      var playerPed = GetPlayerPed(PlayerId());
      StartPlayerSwitch(playerPed, playerPed, 513, 3);
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
      RequestCollisionAtCoord(lastPos.X, lastPos.Y, lastPos.Z);
      SetEntityCoordsNoOffset(ped, lastPos.X, lastPos.Y, lastPos.Z, true, false, false);
      NetworkResurrectLocalPlayer(lastPos.X, lastPos.Y, lastPos.Z, 0, true, false);
      ClearPedTasksImmediately(ped);
      RemoveAllPedWeapons(ped, true);
      ClearPlayerWantedLevel(PlayerId());


      var time = GetGameTimer();
      while (!HasCollisionLoadedAroundEntity(ped) && (GetGameTimer() - time) < 5000)
      {
        await Delay(0);
      }

      if (IsScreenFadedOut())
      {
        DoScreenFadeIn(500);
        while (!IsScreenFadedIn())
        {
          await Delay(5);
        }
      }

      SetPlayerControl(ped, true, 1 << 2);
      FreezePlayer(PlayerId(), false);
      DestroyCam(cameraHandle, false);
      RenderScriptCams(false, false, 1, true, true);
      StopPlayerSwitch();
      DisplayRadar(true);
      DisplayHud(true);
      DisplayCash(true);
      ShowHudELements();
      SpawnLock = false;
    }

    private void OnPlayerSpawned()
    {
      // Get last saved player position from server
      TriggerServerEvent(ServerEvents.GetLastSpawnPosition);
    }

    private void ShowHudELements()
    {
      ShowHudComponentThisFrame((int) HudElements.Cash);
      ShowHudComponentThisFrame((int) HudElements.MpCash);
      ShowHudComponentThisFrame((int) HudElements.AreaName);
      ShowHudComponentThisFrame((int) HudElements.Reticle);
      ShowHudComponentThisFrame((int) HudElements.RadioStations);
      ShowHudComponentThisFrame((int) HudElements.HelpText);
      ShowHudComponentThisFrame((int) HudElements.HudComponents);
      ShowHudComponentThisFrame((int) HudElements.HudWeapons);
      ShowHudComponentThisFrame((int) HudElements.WantedStars);
      ShowHudComponentThisFrame((int) HudElements.Cash);
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