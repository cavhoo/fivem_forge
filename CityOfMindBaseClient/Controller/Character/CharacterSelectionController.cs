extern alias CFX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CFX::CitizenFX.Core;
using CityOfMindClient.Models;
using CityOfMindClient.Models.Character;
using CityOfMindClient.View.UI.Menu.CharacterCreate;
using FiveMForgeClient.Services.Language;
using FiveMForgeClient.View.UI.Menu.CharacterCreate;
using Newtonsoft.Json;
using static CFX::CitizenFX.Core.Native.API;
using BaseScript = CFX::CitizenFX.Core.BaseScript;
using Control = CFX::CitizenFX.Core.Control;
using Debug = CFX::CitizenFX.Core.Debug;
using Game = CFX::CitizenFX.Core.Game;
using Vector3 = CFX::CitizenFX.Core.Vector3;

namespace CityOfMindClient.Controller.Character
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
      TriggerServerEvent(ServerEvents.GetAvailableCharacterSlots);
      
    }
    private void OnCharacterSlotsLoaded(int availableSlots)
    {
      _slotMarkerAmount = availableSlots;
      EventHandlers[ServerEvents.CharactersLoaded] += new Action<string>(OnShowCharacterSelection);
      slotMarkerCoords = CreateSlotMarkerPositions();
      TriggerServerEvent(ServerEvents.LoadCharacters);
      Tick += HandleKeyboardInput;
      _currentCharacterData = new Models.Character.Character("", 99, "", "", new Guid().ToString(),
        $"{_spawnLocation.X}:{_spawnLocation.Y}:{_spawnLocation.Z}");
    }

    private void OnUpdateCharacterModel(Dictionary<string, object> characterData, CallbackDelegate cb)
    {
      Debug.WriteLine($"Updating character {characterData}");
      // Update parents data 
      if (characterData.TryGetValue("mom", out var mom))
      {
        cb(new
        {
          error = "No Parent Data"
        });
        return;
      }

      if (characterData.TryGetValue("dad", out var dad))
      {
        cb(new
        {
          error = "No Parent Data"
        });
        return;
      }

      if (characterData.TryGetValue("faceFactor", out var faceFactor))
      {
        cb(new
        {
          error = "No Parent Data"
        });
        return;
      }

      if (characterData.TryGetValue("skinFactor", out var skinFactor))
      {
        cb(new
        {
          error = "No Parent Data"
        });
        return;
      }


      SetPedBlendFromParents(newCharacterPedId, (int) dad, (int) mom, (float) faceFactor, (float) skinFactor);
      // Update Facial features
      // var facialData = parsedCharacter.FaceData;
      // if (facialData != null)
      // {
      //   Debug.WriteLine($"Updating Nose Length with: {facialData.NoseTipLength} on char: {newCharacterPedId}");
      //   SetPedFaceFeature(newCharacterPedId, 1, facialData.NoseWidth);
      //   SetPedFaceFeature(newCharacterPedId, 2, facialData.NoseTipHeight);
      //   SetPedFaceFeature(newCharacterPedId, 3, facialData.NoseTipLength);
      //   SetPedFaceFeature(newCharacterPedId, 4, facialData.NoseBoneBend);
      //   SetPedFaceFeature(newCharacterPedId, 5, facialData.NoseTipLowering);
      //   SetPedFaceFeature(newCharacterPedId, 6, facialData.NoseBoneOffset);
      //   SetPedFaceFeature(newCharacterPedId, 7, facialData.EyeBrowHeight);
      //   SetPedFaceFeature(newCharacterPedId, 8, facialData.EyeBrowBulkiness);
      //   SetPedFaceFeature(newCharacterPedId, 9, facialData.CheekBoneHeight);
      //   SetPedFaceFeature(newCharacterPedId, 10, facialData.CheekBoneWidth);
      //   SetPedFaceFeature(newCharacterPedId, 11, facialData.CheekWidth);
      //   SetPedFaceFeature(newCharacterPedId, 12, facialData.EyeOpening);
      //   SetPedFaceFeature(newCharacterPedId, 13, facialData.LipThickness);
      //   SetPedFaceFeature(newCharacterPedId, 14, 0.5f); // Jawbones TODO: Make Menu
      //   SetPedFaceFeature(newCharacterPedId, 15, 0.5f); // Jawbones TODO: Make Menu
      //   SetPedFaceFeature(newCharacterPedId, 16, facialData.ChinHeight);
      //   SetPedFaceFeature(newCharacterPedId, 17, facialData.ChinForward);
      //   SetPedFaceFeature(newCharacterPedId, 18, facialData.ChinWidth);
      //   SetPedFaceFeature(newCharacterPedId, 19, facialData.ChinGapSize);
      //   _currentCharacterData.UpdateFaceData(facialData);
      // }
      //
      // // Update hair color and hair
      // var hairData = parsedCharacter.HairData;
      // if (hairData != null)
      // {
      //   SetPedComponentVariation(newCharacterPedId, 2, hairData.HairShape, 0, 2);
      //   SetPedHairColor(newCharacterPedId, hairData.BaseColor, hairData.HighlightColor);
      //   _currentCharacterData.UpdateHairData(hairData);
      // }
      //
      // // Update Makeup
      // var makeUpData = parsedCharacter.MakeUpData;
      // if (makeUpData != null)
      // {
      //   SetPedHeadOverlay(newCharacterPedId, 4, makeUpData.MakeUpVariant, 1.0f);
      //   SetPedHeadOverlay(newCharacterPedId, 5, makeUpData.BlushVariant, 1.0f);
      //   SetPedHeadOverlay(newCharacterPedId, 8, makeUpData.LipstickVariant, 1.0f);
      //   SetPedHeadOverlayColor(newCharacterPedId, 4, 0, makeUpData.MakeUpColor, 0);
      //   SetPedHeadOverlayColor(newCharacterPedId, 5, 2, makeUpData.BlushColor, 0);
      //   SetPedHeadOverlayColor(newCharacterPedId, 8, 2, makeUpData.LipstickColor, 0);
      //   _currentCharacterData.UpdateMakeUpData(makeUpData);
      // }
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
            DeleteEntity(ref removablePedId);
          }

          // Tell SpawnManager to spawn player at last character coords
          TriggerEvent(ClientEvents.SpawnPlayer, _availableCharacters[currentCharacterIndex], SelectionCameraHandle);
        }
        else
        {
          Enabled = false;
          // TODO: Replace with model information from character data...
          var modelHashKey = GetHashKey("mp_m_freemode_01");
          RequestModel((uint) modelHashKey);
          while (!HasModelLoaded((uint) modelHashKey))
          {
            await Delay(5);
          }

          newCharacterPedId = CreatePed(2, (uint) modelHashKey, _spawnLocation.X, _spawnLocation.Y, _spawnLocation.Z, 0,
            false, true);
          TriggerEvent(ClientEvents.ShowCharacterCreationMenu, newCharacterPedId, SelectionCameraHandle);
          PointCamAtCoord(SelectionCameraHandle, _spawnLocation.X, _spawnLocation.Y, _spawnLocation.Z);
          SetCamCoord(SelectionCameraHandle, _spawnLocation.X, _spawnLocation.Y + 2.0f, _spawnLocation.Z + 2.0f);
        }
      }
    }

    private void UpdateCharacterCamera()
    {
      var selectedCharPos = slotMarkerCoords[currentCharacterIndex];
      var camPosX = (_slotMarkerRadius + 5) *
                    Math.Cos(FiveMForgeClient.Utils.Math.ToRadians((360 / slotMarkerCoords.Count()) *
                                                                   currentCharacterIndex));
      var camPosY = (_slotMarkerRadius + 5) *
                    Math.Sin(FiveMForgeClient.Utils.Math.ToRadians((360 / slotMarkerCoords.Count()) *
                                                                   currentCharacterIndex));

      SetCamCoord(SelectionCameraHandle, _spawnLocation.X - (float) camPosX, _spawnLocation.Y - (float) camPosY,
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
      //DisplayHud(false);
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
        var x = _slotMarkerRadius * Math.Cos(FiveMForgeClient.Utils.Math.ToRadians((360 / charAmount) * i));
        var y = _slotMarkerRadius * Math.Sin(FiveMForgeClient.Utils.Math.ToRadians((360 / charAmount) * i));
        var pedChar = CreatePed(2, (uint) modelHashKey, _spawnLocation.X - (float) x, _spawnLocation.Y - (float) y,
          328.17358f, 90.0f, false, true);
        _createdCharacters.Add(pedChar);
      }

      var firstCharacterPos = GetEntityCoords(_createdCharacters[0], true);
      var camPosX = (_slotMarkerRadius + 5) * Math.Cos(FiveMForgeClient.Utils.Math.ToRadians((360 / charAmount) * 0));
      var camPosY = (_slotMarkerRadius + 5) * Math.Sin(FiveMForgeClient.Utils.Math.ToRadians((360 / charAmount) * 0));

      SetCamCoord(SelectionCameraHandle, _spawnLocation.X - (float) camPosX, _spawnLocation.Y - (float) camPosY,
        firstCharacterPos.Z);
      PointCamAtCoord(SelectionCameraHandle, firstCharacterPos.X, firstCharacterPos.Y, _spawnLocation.Z);
    }

    private List<Vector3> CreateSlotMarkerPositions()
    {
      var markerList = new List<Vector3>();
      for (var i = 0; i < _slotMarkerAmount; i++)
      {
        var x = _slotMarkerRadius * Math.Cos(FiveMForgeClient.Utils.Math.ToRadians((360 / _slotMarkerAmount) * i));
        var y = _slotMarkerRadius * Math.Sin(FiveMForgeClient.Utils.Math.ToRadians((360 / _slotMarkerAmount) * i));
        markerList.Add(new Vector3(_spawnLocation.X - (float) x, _spawnLocation.Y - (float) y,
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
          Convert.ToString(character["CharacterUuid"]),
          Convert.ToString(character["LastPos"])
        ));
      }

      return charList;
    }
  }
}