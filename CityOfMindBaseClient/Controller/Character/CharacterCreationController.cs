extern alias CFX;
using System;
using System.Collections;
using System.Collections.Generic;
using CFX::CitizenFX.Core;
using CityOfMindClient.Models;
using CityOfMindClient.Models.Character;
using Newtonsoft.Json;
using static CFX::CitizenFX.Core.Native.API;

namespace CityOfMindClient.Controller.Character
{
  public class CharacterCreationController : BaseScript
  {
    private readonly Vector3 _spawnLocation = new(-74.95219f, -818.7512f, 326.0000f);
    private readonly Vector3 _faceCamPosition = new(-74.95219f, -818.2512f, 326.0000f);
    private readonly Vector3 _torsoCamPosition = new(-74.95219f, -818.2512f, 326.0000f);
    private readonly Vector3 _legCamPosition = new(-74.95219f, -818.2512f, 326.0000f);
    private readonly Vector3 _feetCamPosition = new(-74.95219f, -818.2512f, 326.0000f);

    private int CharacterCameraHandle;
    private int NewCharacterId;
    private Models.Character.Character CharacterData;

    public CharacterCreationController()
    {
      CharacterData = new Models.Character.Character();
      EventHandlers[ClientEvents.ShowCharacterCreationMenu] += new Action<int>(OnShowCharacterCreation);
      EventHandlers[ServerEvents.CharacterSaved] += new Action<string>(OnCharacterCreated);
      RegisterNuiCallbackType("updateCharacter"); // register the type
      EventHandlers["__cfx_nui:updateCharacter"] +=
        new Action<IDictionary<string, object>, CallbackDelegate>(async (data, cb) =>
        {
          var gender = Convert.ToString(data["gender"]);
          if (gender != CharacterData.Gender)
          {
            CharacterData.Gender = gender;
            var modelHash = GetHashKey(gender == "male" ? "mp_m_freemode_01" : "mp_f_freemode_01");
            RequestModel((uint)modelHash);
            while (!HasModelLoaded((uint)modelHash))
            {
              await Delay(5);
            }

            SetPlayerModel(PlayerId(), (uint)modelHash);
            SetModelAsNoLongerNeeded((uint)modelHash);
          }

          // Update Parent Data
          CharacterData.FaceResemblence = Convert.ToSingle(data["faceFactor"]);
          CharacterData.SkinToneResemblence = Convert.ToSingle(data["skinFactor"]);
          CharacterData.Dad = Convert.ToInt32(data["dad"]);
          CharacterData.Mom = Convert.ToInt32(data["mom"]);
          CharacterData.Firstname = Convert.ToString(data["firstname"]);
          CharacterData.Lastname = Convert.ToString(data["lastname"]);
          CharacterData.Birthdate = Convert.ToString(data["birthdate"]);
          // Update Facial Data
          var noseData = (IDictionary<string, object>)data["nose"];
          var noseWidth = Convert.ToSingle(noseData["width"]);
          var noseTipHeight = Convert.ToSingle(noseData["tipHeight"]);
          var noseTipLength = Convert.ToSingle(noseData["tipLength"]);
          var noseBoneBend = Convert.ToSingle(noseData["boneBend"]);
          var noseBoneOffset = Convert.ToSingle(noseData["boneOffset"]);
          var noseTipLowering = Convert.ToSingle(noseData["tipLowering"]);
          CharacterData.UpdateNoseData(noseWidth, noseTipHeight, noseBoneBend, noseBoneOffset, noseTipLength,
            noseTipLowering);

          // Update Eye Data
          var eyeData = (IDictionary<string, object>)data["eyes"];
          var eyeColor = Convert.ToInt32(eyeData["color"]);
          var eyeOpening = Convert.ToInt32(eyeData["opening"]);
          var browBulkiness = Convert.ToSingle(eyeData["browBulkiness"]);
          var browHeight = Convert.ToSingle(eyeData["browHeight"]);
          var browStyle = Convert.ToInt32(eyeData["eyeBrowStyle"]);
          var browColor = Convert.ToInt32(eyeData["eyeBrowColor"]);
          CharacterData.UpdateEyeData(eyeColor, browHeight, browBulkiness, eyeOpening, browStyle, browColor);

          // Update Cheek Data
          var cheekData = (IDictionary<string, object>)data["cheeks"];
          CharacterData.CheekBoneWidth = Convert.ToSingle(cheekData["cheekBoneWidth"]);
          CharacterData.CheekBoneHeight = Convert.ToSingle(cheekData["cheekBoneHeight"]);
          CharacterData.CheekWidth = Convert.ToSingle(cheekData["cheekWidth"]);
          // Update lip data
          CharacterData.LipsThickness = Convert.ToSingle(data["lipThickness"]);

          // Update Makeup
          CharacterData.MakeUpVariant = Convert.ToInt32(data["makeUpVariant"]);
          CharacterData.MakeUpColor = Convert.ToInt32(data["makeUpColor"]);

          // Update Hairstyle
          CharacterData.HairShape = Convert.ToInt32(data["hairStyle"]);
          CharacterData.HairColor = Convert.ToInt32(data["hairBaseColor"]);
          CharacterData.HairHighlightColor = Convert.ToInt32(data["hairHighlightColor"]);

          // Update Beard
          CharacterData.BeardShape = Convert.ToInt32(data["beardStyle"]);
          CharacterData.BeardColor = Convert.ToInt32(data["beardColor"]);

          // Update Blush
          CharacterData.BlushVariant = Convert.ToInt32(data["blushVariant"]);
          CharacterData.BlushColor = Convert.ToInt32(data["blushColor"]);

          // Update Lipstick
          CharacterData.LipstickVariant = Convert.ToInt32(data["lipStickVariant"]);
          CharacterData.LipstickColor = Convert.ToInt32(data["lipStickColor"]);

          // Update Chest Hair
          CharacterData.ChestHairShape = Convert.ToInt32(data["chestHairVariant"]);
          CharacterData.ChestHairColor = Convert.ToInt32(data["chestHairColor"]);

          // Update Character Data
          Character.UpdateProperties(PlayerPedId(), CharacterData);
          cb(new object[] { "ok" });
        });
      RegisterNuiCallbackType("character/highlightBodyPart");
      EventHandlers["__cfx_nui:highlightBodyPart"] +=
        new Action<IDictionary<string, object>, CallbackDelegate>(OnHighlightBodyPart);

      RegisterNuiCallbackType("character/getInitialData");
      EventHandlers["__cfx_nui:getInitialData"] +=
        new Action<IDictionary<string, object>, CallbackDelegate>(OnInitalDataRequested);

      RegisterNuiCallbackType("character/createCharacter");
      EventHandlers["__cfx_nui:createCharacter"] +=
        new Action<IDictionary<string, object>, CallbackDelegate>(OnCreateCharacter);
    }

    protected void OnCharacterCreated(string charUuid)
    {
      SendNuiMessage(JsonConvert.SerializeObject(new
           {
             targetUI = "characterCreator",
             payload = new
             {
               eventType = "close",
             },
           }));
      
      SetEntityVisible(GetPlayerPed(PlayerId()), false, true);
      SetNuiFocus(false, false);
    }

    protected void OnCreateCharacter(IDictionary<string, object> data, CallbackDelegate callbackDelegate)
    {
      Debug.WriteLine("Creating new character");
      TriggerServerEvent(ServerEvents.CreateCharacter, JsonConvert.SerializeObject(CharacterData));
      callbackDelegate(new { status = "ok" });
    }

    protected void OnHighlightBodyPart(IDictionary<string, object> data, CallbackDelegate callback)
    {
      var bodypartToHighlight = (string)data["bodypart"];
      switch (bodypartToHighlight)
      {
        case "Head":
          SetCamCoord(CharacterCameraHandle, _faceCamPosition.X, _faceCamPosition.Y, _faceCamPosition.Z);
          break;
        case "Torso":
          SetCamCoord(CharacterCameraHandle, _torsoCamPosition.X, _torsoCamPosition.Y, _torsoCamPosition.Z);
          break;
        case "Legs":
          SetCamCoord(CharacterCameraHandle, _legCamPosition.X, _legCamPosition.Y, _legCamPosition.Z);
          break;
        case "Feet":
          SetCamCoord(CharacterCameraHandle, _feetCamPosition.X, _feetCamPosition.Y, _feetCamPosition.Z);
          break;
        default: // Zoom back out
          SetCamCoord(CharacterCameraHandle, _spawnLocation.X, _spawnLocation.Y + 2.0f, _spawnLocation.Z);
          break;
      }

      callback(new { status = "ok" });
    }

    protected async void OnShowCharacterCreation(int cameraHandle)
    {
      CharacterCameraHandle = cameraHandle;
      PointCamAtCoord(cameraHandle, _spawnLocation.X, _spawnLocation.Y, _spawnLocation.Z);
      SetCamCoord(cameraHandle, _spawnLocation.X, _spawnLocation.Y + 2.0f, _spawnLocation.Z);
      SendNuiMessage(JsonConvert.SerializeObject(new
      {
        targetUI = "characterCreator",
        payload = new
        {
          eventType = "open",
        },
      }));

      var modelHashKey = GetHashKey("mp_f_freemode_01");
      RequestModel((uint)modelHashKey);
      while (!HasModelLoaded((uint)modelHashKey))
      {
        await Delay(5);
      }

      NewCharacterId = PlayerId();
      //NewCharacterId = CreatePed(0, (uint) modelHashKey, _spawnLocation.X, _spawnLocation.Y, _spawnLocation.Z, 0, false,
      //false);
      SetPlayerModel(NewCharacterId, (uint)modelHashKey);
      SetEntityVisible(PlayerPedId(), true, true);
      SetEntityCoords(PlayerPedId(), _spawnLocation.X, _spawnLocation.Y, _spawnLocation.Z, true, true, false, false);
      SetNuiFocus(true, true);
    }

    protected void OnInitalDataRequested(IDictionary<string, object> data, CallbackDelegate cb)
    {
      Debug.WriteLine("Sending intial Data");
      cb(JsonConvert.SerializeObject(new
      {
        targetUI = "characterCreator",
        payload = new
        {
          eventType = "init",
          eventData = new
          {
            moms = CharacterComponents.InheritanceMoms,
            dads = CharacterComponents.InheritanceDads,
            makeUpVariants = CharacterComponents.MakeUp,
            beardStyles = CharacterComponents.BeardStyles,
            maleHairStyles = CharacterComponents.MaleHairStyles,
            femaleHairStyles = CharacterComponents.FemaleHairStyles,
            hairColors = GetAvailableHairColors().ToArray(),
            makeUpColors = GetAvailableMakeUpColors().ToArray(),
            tattooVariantsMale = GetTattoIndeces(3), // 3 = MPMale
            tattooVariantsFemale = GetTattoIndeces(4), // 4 = MPFemale
            lipStickColors = GetAvailableMakeUpColors().ToArray(),
          }
        }
      }));
    }

    protected List<Color> GetAvailableMakeUpColors()
    {
      var colors = new List<Color>();
      var availableColors = GetNumMakeupColors();

      for (var i = 0; i < availableColors; i++)
      {
        var r = 0;
        var g = 0;
        var b = 0;
        GetPedMakeupRgbColor(i, ref r, ref g, ref b);
        colors.Add(new Color(i, r, g, b));
      }

      return colors;
    }

    protected int GetTattoIndeces(int characterType)
    {
      return GetNumTattooShopDlcItems(characterType);
    }

    protected List<Color> GetAvailableHairColors()
    {
      var colors = new List<Color>();
      var availableColors = GetNumHairColors();
      for (var i = 0; i < availableColors; i++)
      {
        var r = 0;
        var g = 0;
        var b = 0;
        GetPedHairRgbColor(i, ref r, ref g, ref b);
        colors.Add(new Color(i, r, g, b));
      }

      return colors;
    }
  }
}