using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using Client.Enums;
using Client.Models;
using Client.Utils;
using Newtonsoft.Json;
using static CitizenFX.Core.Native.API;
using BaseScript = CitizenFX.Core.BaseScript;
using Math = System.Math;
using Vector3 = CitizenFX.Core.Vector3;

namespace Client.Controller.Spawn
{
  public class SpawnController : BaseScript
  {
    private readonly Vector3 _spawnLocation = new(-1046.6901f, -2770.3647f, 4.62854f);
    private bool ForceRespawn = true;
    private int TimeOfDeath = -1;
    private bool Instantiated { get; set; }
    private bool SpawnLock { get; set; }

    public SpawnController()
    {
      //EventHandlers[ClientEvents.SpawnPlayer] += new Action<string, int>(SpawnPlayer);
      EventHandlers[ClientEvents.ScriptStart] += new Action<string, int>(SpawnPlayer);
      //Tick += SpawnLoop;
      //TriggerServerEvent(ServerEvents.GetLastSpawnPosition);
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
          SetEntityVisible(ped, true, true);
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

    private async void SpawnPlayer(string character, int cameraHandle)
    {
      if (Instantiated)
      {
        return;
      }
      ShutdownLoadingScreen();
      Instantiated = true;
      Debug.WriteLine("Trying to spawn player");
      //var characterToSpawn = JsonConvert.DeserializeObject<Models.Character.Character>(character);
      // Parsing X:Y:Z to Vector 3
      //var lastPos = Parser.StringToVector3(characterToSpawn?.LastPos);
      var lastPos = _spawnLocation;
      FreezePlayer(PlayerId(), true);
      var modelHashKey = GetHashKey("mp_m_freemode_01"); //GetHashKey(characterToSpawn?.Gender == "male" ? "mp_m_freemode_01" : "mp_f_freemode_01");
      RequestModel((uint)modelHashKey);
      while (!HasModelLoaded((uint)modelHashKey))
      { 
        RequestModel((uint)modelHashKey);
        await Delay(15);
      }
      SetPlayerModel(PlayerId(), (uint)modelHashKey);
      var ped = PlayerPedId();
      Vector3 sourceLocation = new(-74.95219f, -818.7512f, 326.0000f);
      //var switchType = GetIdealPlayerSwitchType(sourceLocation.X, sourceLocation.Y, sourceLocation.Z, lastPos.X,
        //lastPos.Y, lastPos.Z + 0.5f);
      //StartPlayerSwitch(ped, ped, 2050, switchType);
      SetModelAsNoLongerNeeded((uint)modelHashKey);
      SetPedRandomComponentVariation(ped, true);
      //Client.Controller.Character.Character.UpdateProperties(ped, characterToSpawn);

      RequestCollisionAtCoord(lastPos.X, lastPos.Y, lastPos.Z + 0.5f);
      SetEntityCoordsNoOffset(ped, lastPos.X, lastPos.Y, lastPos.Z + 0.5f, true, false, false);
      NetworkResurrectLocalPlayer(lastPos.X, lastPos.Y, lastPos.Z, 0, true, false);
      ClearPedTasksImmediately(ped);
      ClearPlayerWantedLevel(PlayerId());
      TriggerEvent(ClientEvents.PlayerSpawned);
      var time = GetGameTimer();
      while (!HasCollisionLoadedAroundEntity(ped) && (GetGameTimer() - time) < 5000)
      {
        await Delay(5);
      }
 
      SetPlayerControl(ped, true, 1 << 2);
      FreezePlayer(PlayerId(), false);
      SetEntityVisible(PlayerPedId(), true, false);
      //DestroyCam(cameraHandle, false);
      RenderScriptCams(false, false, 1, true, true);
      //StopPlayerSwitch();
      DisplayRadar(true);
      DisplayHud(true);
      DisplayCash(true);
      ShowHudELements();
      SpawnLock = false;
    }

    private void OnPlayerSpawned()
    {
      // Get last saved player position from server
      //TriggerServerEvent(ServerEvents.GetLastSpawnPosition);
    }

    private void ShowHudELements()
    {
      ShowHudComponentThisFrame((int)HudElements.Cash);
      ShowHudComponentThisFrame((int)HudElements.MpCash);
      ShowHudComponentThisFrame((int)HudElements.AreaName);
      ShowHudComponentThisFrame((int)HudElements.Reticle);
      ShowHudComponentThisFrame((int)HudElements.RadioStations);
      ShowHudComponentThisFrame((int)HudElements.HelpText);
      ShowHudComponentThisFrame((int)HudElements.HudComponents);
      ShowHudComponentThisFrame((int)HudElements.HudWeapons);
      ShowHudComponentThisFrame((int)HudElements.WantedStars);
      ShowHudComponentThisFrame((int)HudElements.Cash);
    }
  }
}
