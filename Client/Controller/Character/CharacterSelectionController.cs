extern alias CFX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Models;
using Client.Services.Language;
using Newtonsoft.Json;
using static CFX::CitizenFX.Core.Native.API;
using BaseScript = CFX::CitizenFX.Core.BaseScript;
using Control = CFX::CitizenFX.Core.Control;
using Debug = CFX::CitizenFX.Core.Debug;
using Game = CFX::CitizenFX.Core.Game;
using Vector3 = CFX::CitizenFX.Core.Vector3;

namespace Client.Controller.Character
{
  public class CharacterSelectionController : BaseScript
  {
    private bool Instantiated;
    private bool Enabled;
    private int currentCharacterIndex;
    private int newCharacterPedId;
    private int SelectionCameraHandle = -1;
    private readonly Vector3 _spawnLocation = new(-74.95219f, -818.7512f, 326.0000f);
    private const int _slotMarkerRadius = 6;
    private int _slotMarkerAmount = 8;
    private readonly List<int> _createdCharacters = new();
    private List<Vector3> slotMarkerCoords;
    private List<Models.Character.Character> _availableCharacters;
    private Models.Character.Character _currentCharacterData;

    public CharacterSelectionController()
    {
      EventHandlers[ClientEvents.CharacterCreationClosed] += new Action<dynamic>(obj => Enabled = true);
      EventHandlers[ServerEvents.AvailableCharacterSlotsLoaded] += new Action<int>(OnCharacterSlotsLoaded);
      EventHandlers[ServerEvents.CharacterSaved] += new Action<string>(OnCharacterSaved);
      TriggerServerEvent(ServerEvents.GetAvailableCharacterSlots);
    }

    private void OnCharacterSaved(string charUuid)
    {
      TriggerServerEvent(ServerEvents.LoadCharacters);
    }

    private void OnCharacterSlotsLoaded(int availableSlots)
    {
      _slotMarkerAmount = availableSlots;
      EventHandlers[ServerEvents.CharactersLoaded] += new Action<string>(OnShowCharacterSelection);
      slotMarkerCoords = CreateSlotMarkerPositions();
      TriggerServerEvent(ServerEvents.LoadCharacters);
      Tick += HandleKeyboardInput;
    }

    private async Task HandleKeyboardInput()
    {
      if (!Enabled) return;
      if (Game.IsControlJustPressed(0, Control.FrontendLeft))
      {
        if (currentCharacterIndex == 0)
        {
          currentCharacterIndex = slotMarkerCoords.Count() - 1;
        }
        else
        {
          currentCharacterIndex--;
        }

        UpdateCharacterCamera();
      }

      if (Game.IsControlJustPressed(0, Control.FrontendRight))
      {
        if (currentCharacterIndex == slotMarkerCoords.Count() - 1)
        {
          currentCharacterIndex = 0;
        }
        else
        {
          currentCharacterIndex++;
        }

        if (currentCharacterIndex >= _availableCharacters.Count())
        {
          BeginTextCommandDisplayHelp("STRING");
          AddTextComponentSubstringPlayerName(LanguageService.Translate("create_character_here"));
          EndTextCommandDisplayHelp(0, false, true, -1);
        }

        UpdateCharacterCamera();
      }

      if (Game.IsControlJustPressed(0, Control.FrontendAccept) || Game.IsControlJustPressed(0, Control.Enter))
      {
        if (currentCharacterIndex < _availableCharacters.Count())
        {
          // Remove handler to draw slot marker
          Tick -= DrawCharacterSlotMarker;
          // Remove Keyboard handling
          Tick -= HandleKeyboardInput;

          // Remove all spawned characters from roof top
          for (var i = 0; i < _createdCharacters.Count(); i++)
          {
            var removablePedId = _createdCharacters[i];
            //DeleteEntity(ref removablePedId);
          }

          // Tell SpawnManager to spawn player at last character coords
          TriggerEvent(ClientEvents.SpawnPlayer,
            JsonConvert.SerializeObject(_availableCharacters[currentCharacterIndex]), SelectionCameraHandle);
        }
        else
        {
          Enabled = false;
          TriggerEvent(ClientEvents.ShowCharacterCreationMenu, SelectionCameraHandle);
        }
      }
    }

    private void UpdateCharacterCamera()
    {
      var selectedCharPos = slotMarkerCoords[currentCharacterIndex];
      var camPosX = (_slotMarkerRadius + 5) *
                    Math.Cos(Utils.Math.ToRadians((360 / slotMarkerCoords.Count()) *
                                                  currentCharacterIndex));
      var camPosY = (_slotMarkerRadius + 5) *
                    Math.Sin(Utils.Math.ToRadians((360 / slotMarkerCoords.Count()) *
                                                  currentCharacterIndex));

      SetCamCoord(SelectionCameraHandle, _spawnLocation.X - (float)camPosX, _spawnLocation.Y - (float)camPosY,
        328.17358f);
      PointCamAtCoord(SelectionCameraHandle, selectedCharPos.X, selectedCharPos.Y, _spawnLocation.Z);
    }

    private async void OnShowCharacterSelection(string characters)
    {
      Enabled = true;
      _availableCharacters =
        JsonConvert.DeserializeObject<Models.Character.Character[]>(characters).ToList(); //JSON.Parse(characters);
      var playerPed = PlayerPedId();
      SetPlayerControl(playerPed, false, 1 << 2);
      FreezeEntityPosition(playerPed, true);
      SetEntityCoords(playerPed, _spawnLocation.X, _spawnLocation.Y, _spawnLocation.Z, false, false, false, true);
      SetEntityVisible(playerPed, false, false);
      Tick += DrawCharacterSlotMarker; // Draw markers in a circle based on how many characters an account can have.
      DisplayRadar(false);
      DisplayHud(false);
      DisableAllControlActions(0);
      DisableFirstPersonCamThisFrame();
      ShutdownLoadingScreen();
      DoScreenFadeOut(500);

      while (!IsScreenFadedOut())
      {
        await Delay(5);
      }

      if (SelectionCameraHandle < 0)
      {
        SelectionCameraHandle = CreateCamWithParams(CameraTypes.DEFAULT_SCRIPTED_CAMERA, _spawnLocation.X,
          _spawnLocation.Y,
          328.17358f, 0, 0, 0,
          GetGameplayCamFov(), false, 1);
      }

      SpawnCharacters(_availableCharacters);
      SetCamActive(SelectionCameraHandle, true);
      RenderScriptCams(true, true, 1000, false, false);
      TriggerEvent(ClientEvents.ShowCharacterInformation, true);
      DoScreenFadeIn(500);
      while (!IsScreenFadedIn())
      {
        await Delay(5);
      }
    }

    private async void SpawnCharacters(List<Models.Character.Character> characters)
    {
      var charAmount = characters.Count();
      for (var i = 0; i < charAmount; i++)
      {
        var modelHashKey = GetHashKey(characters[i].Gender == "male" ? "mp_m_freemode_01" : "mp_f_freemode_01");
        RequestModel((uint)modelHashKey);
        Debug.WriteLine($"{(uint)modelHashKey}");
        while (!HasModelLoaded((uint)modelHashKey))
        {
          await Delay(15);
        }

        var x = _slotMarkerRadius * Math.Cos(Utils.Math.ToRadians((360 / _slotMarkerAmount) * i));
        var y = _slotMarkerRadius * Math.Sin(Utils.Math.ToRadians((360 / _slotMarkerAmount) * i));
        var pedChar = CreatePed(2, (uint)modelHashKey, _spawnLocation.X - (float)x, _spawnLocation.Y - (float)y,
          328.17358f, 90.0f, false, true);
        Client.Controller.Character.Character.UpdateProperties(pedChar, characters[i]);
        SetModelAsNoLongerNeeded((uint)modelHashKey);
        _createdCharacters.Add(pedChar);
      }

      var firstCharacterPos = slotMarkerCoords.First();
      var camPosX = (_slotMarkerRadius + 5) *
                    Math.Cos(Utils.Math.ToRadians((360 / _slotMarkerAmount) * 0));
      var camPosY = (_slotMarkerRadius + 5) *
                    Math.Sin(Utils.Math.ToRadians((360 / _slotMarkerAmount) * 0));

      SetCamCoord(SelectionCameraHandle, _spawnLocation.X - (float)camPosX, _spawnLocation.Y - (float)camPosY,
        firstCharacterPos.Z);
      PointCamAtCoord(SelectionCameraHandle, firstCharacterPos.X, firstCharacterPos.Y, _spawnLocation.Z);
    }

    private List<Vector3> CreateSlotMarkerPositions()
    {
      var markerList = new List<Vector3>();
      for (var i = 0; i < _slotMarkerAmount; i++)
      {
        var x = _slotMarkerRadius * Math.Cos(Utils.Math.ToRadians((360 / _slotMarkerAmount) * i));
        var y = _slotMarkerRadius * Math.Sin(Utils.Math.ToRadians((360 / _slotMarkerAmount) * i));
        markerList.Add(new Vector3(_spawnLocation.X - (float)x, _spawnLocation.Y - (float)y,
          _spawnLocation.Z - 1.0f));
      }

      return markerList;
    }

    private async Task DrawCharacterSlotMarker()
    {
      if (!Enabled) return;
      for (var i = 0; i < slotMarkerCoords.Count(); i++)
      {
        var cords = slotMarkerCoords[i];
        DrawMarker(1, cords.X, cords.Y, cords.Z, 0.0f, 0.0f, 0.0f,
          0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f,
          119, 168, 186,
          255, false, false, 2, false, null, null, false);
      }
    }
  }
}