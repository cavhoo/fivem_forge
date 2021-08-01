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
    public CharacterCreationController()
    {
      EventHandlers[ClientEvents.ShowCharacterCreationMenu] += new Action<int, int>(OnShowCharacterCreation);
      RegisterNuiCallbackType("updateCharacter"); // register the type
      EventHandlers["__cfx_nui:updateCharacter"] +=
        new Action<IDictionary<string, object>, CallbackDelegate>((data, cb) =>
        {
          Debug.WriteLine("Character Data received");
          cb(new object[] {"ok"});
        });
      RegisterNuiCallbackType("highlightBodyPart");
      EventHandlers["__cfx_nui:highlightBodyPart"] += new Action<IDictionary<string, object>, CallbackDelegate>(OnHighlightBodyPart);
      
      RegisterNuiCallbackType("getInitialData");
      EventHandlers["__cfx_nui:getInitialData"] +=
        new Action<IDictionary<string, object>, CallbackDelegate>(OnInitalDataRequested);
    }

    protected void OnHighlightBodyPart(IDictionary<string, object> data, CallbackDelegate callback)
    {
      var bodypartToHighlight = (string)data["bodypart"];
      switch (bodypartToHighlight)
      {
        case "Head":
          break;
        case "Torso":
          break;
        case "Legs":
          break;
        case "Feet":
          break;
      }
      
      callback(new {status = "ok"});
    }

    protected void OnShowCharacterCreation(int createdPed, int cameraHandle)
    {
      Debug.WriteLine("Showing character menu");
      SendNuiMessage(JsonConvert.SerializeObject(new
      {
        targetUI = "characterCreator",
        payload = new
        {
          eventType = "open",
        },
      }));
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