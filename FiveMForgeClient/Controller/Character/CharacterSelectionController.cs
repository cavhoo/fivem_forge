extern alias CFX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CFX::CitizenFX.Core;
using fastJSON;
using FiveMForgeClient.Models;
using static CFX::CitizenFX.Core.Native.API;

namespace FiveMForgeClient.Controller.Character
{
  public class CharacterSelectionController : BaseScript
  {
    private bool Instantiated = false;
    private int SelectionCameraHandle = -1;
    private Vector3 SpawnLocation = new Vector3(-74.95219f, -818.7512f, 326.0000f);
    private int SlotMarkerRadius = 6;
    private int SlotMarkerAmount = 2;
    private List<int> createdCharacters = new List<int>();

    public CharacterSelectionController()
    {
      EventHandlers[ClientEvents.ScriptStart] += new Action<string>(OnScriptStart);
    }

    private void OnScriptStart(string resourceName)
    {
      if (Instantiated) return;
      Instantiated = true;
      EventHandlers[ServerEvents.CharactersLoaded] += new Action<string>(OnShowCharacterSelection);
      TriggerServerEvent(ServerEvents.LoadCharacters);
    }

    private async void OnShowCharacterSelection(string characters)
    {
      var availCharacter = JSON.Parse(characters);
      if (!(availCharacter is IEnumerable characterList)) return;
      var playerPed = PlayerPedId();
      SetPlayerControl(playerPed, false, 1 << 2);
      FreezeEntityPosition(playerPed, true);
      SetEntityCoords(playerPed, SpawnLocation.X, SpawnLocation.Y, SpawnLocation.Z, false, false, false, true);
      SetEntityVisible(playerPed, false, false);
      Tick += DrawCharacterSlotMarker; // Draw markers in a circle based on how many characters an account can have.
      DisplayHud(false);
      DisplayRadar(false);
      DisableAllControlActions(2);
      DisableFirstPersonCamThisFrame();
      ShutdownLoadingScreen();
      DoScreenFadeOut(500);
      while (!IsScreenFadedOut())
      {
        await Delay(5);
      }

      if (SelectionCameraHandle < 0)
      {
        SelectionCameraHandle = CreateCamWithParams(CameraTypes.DEFAULT_SCRIPTED_CAMERA, SpawnLocation.X, SpawnLocation.Y,
          SpawnLocation.Z + 20, 0, 0, 0,
          GetGameplayCamFov(), false, 1);
      }

      SpawnCharacters(ParseCharacterList(characterList));
      SetCamActive(SelectionCameraHandle, true);
      PointCamAtCoord(SelectionCameraHandle, SpawnLocation.X, SpawnLocation.Y, SpawnLocation.Z);
      RenderScriptCams(true, true, 1000, true, false);
      DoScreenFadeIn(500);
      while (!IsScreenFadedIn())
      {
        await Delay(5);
      }
    }

    private async void SpawnCharacters(IEnumerable<Models.Character.Character> characters)
    {
      // TODO: Replace with model information from character data...
      var modelHashKey = GetHashKey("a_m_y_hipster_01");
      RequestModel((uint) modelHashKey);
      while (!HasModelLoaded((uint) modelHashKey))
      {
        await Delay(500);
      }
      var charAmount = characters.Count();
      for (var i = 0; i < charAmount; i++)
      {
        var x = SlotMarkerRadius * Math.Cos(Utils.Math.ToRadians((360 / charAmount) * i));
        var y = SlotMarkerRadius * Math.Sin(Utils.Math.ToRadians((360 / charAmount) * i));
        var pedChar = CreatePed(2, (uint) modelHashKey, SpawnLocation.X - (float) x, SpawnLocation.Y - (float) y, 328.17358f, 90.0f, false, true);
        createdCharacters.Add(pedChar);
      }

      var firstCharacterPos = GetEntityCoords(createdCharacters[0], true);
      SetCamCoord(SelectionCameraHandle, firstCharacterPos.X, firstCharacterPos.Y, firstCharacterPos.Z);
    }

    private async Task DrawCharacterSlotMarker()
    {
      for (var i = 0; i < SlotMarkerAmount; i++)
      {
        var x = SlotMarkerRadius * Math.Cos(Utils.Math.ToRadians((360 / SlotMarkerAmount) * i));
        var y = SlotMarkerRadius * Math.Sin(Utils.Math.ToRadians((360 / SlotMarkerAmount) * i)); 
        DrawMarker(1,SpawnLocation.X - (float)  x, SpawnLocation.Y - (float) y, 327.17358f - 1.8f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f,
          255, 0, 0,
          255, false, false, 2, false, null, null, false);
      }
    }

    private List<Models.Character.Character> ParseCharacterList(IEnumerable characterList)
    {
      var charList = new List<Models.Character.Character>();
      foreach (Dictionary<string, object> character in characterList)
      {
        charList.Add(new(
          Convert.ToString(character["Name"]),
          Convert.ToInt32(character["Age"]),
          Convert.ToString(character["AccountUuid"]),
          Convert.ToString(character["JobUuid"]),
          Convert.ToString(character["CharacterUuid"])
        ));
      }

      return charList;
    }
 }
}