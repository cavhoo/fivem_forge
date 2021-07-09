extern alias CFX;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CFX::CitizenFX.Core;
using CFX::CitizenFX.Core.UI;
using CFX::System.Drawing;
using FiveMForgeClient.Controller.Character;
using FiveMForgeClient.Controller.Language;
using FiveMForgeClient.Models;
using FiveMForgeClient.View.UI.Hud;
using FiveMForgeClient.View.UI.Menu;
using NativeUI;

namespace FiveMForgeClient.View
{
  public enum MenuIds
  {
    CarSpawner,
    MoneyTransfer,
    WeaponSpawner,
    CharacterCreator,
  }

  public enum HudIds
  {
    Speedometer,
    CharacterInformation
  }


  public class UIMain : BaseScript
  {
    private bool _initialized = false;
    private MenuPool _pool;
    private Dictionary<MenuIds, UIMenu> _menus;
    private Dictionary<HudIds, Base> _hudElements;

    public UIMain()
    {
      _menus = new Dictionary<MenuIds, UIMenu>();
      EventHandlers[ClientEvents.ScriptStart] += new Action<string>(CreateUi);
      EventHandlers[ClientEvents.ShowCharacterInformation] += new Action<bool>(ShowCharacterInformation);
      EventHandlers[ClientEvents.ShowCharacterCreationMenu] += new Action<bool>(ShowCharacterCreationMenu);
    }

    private void CreateUi(string resourceName)
    {
      if (_initialized) return;
      _hudElements = new Dictionary<HudIds, Base>();
      _menus = new Dictionary<MenuIds, UIMenu>();
      _initialized = true;
      _hudElements.Add(HudIds.Speedometer, new Speedometer());
      _hudElements.Add(HudIds.CharacterInformation, new CharacterInformation());
      _menus.Add(MenuIds.CarSpawner, new CarSpawnMenu("Car Spawner", "Spawn a car"));
      _pool = new MenuPool();


      var characterCreator = new CharacterCreator(LanguageController.Translate("character_creator"),
        LanguageController.Translate("character_creator_description"));
      
      characterCreator.SetMenuPool(ref _pool);
      characterCreator.Visible = false;
      _menus.Add(MenuIds.CharacterCreator, characterCreator);
      foreach (var value in _menus.Values)
      {
        _pool.Add(value);
      }

      Tick += async () =>
      {
        _pool.ProcessMenus();
        foreach (var hudElementsValue in _hudElements.Values)
        {
          hudElementsValue.Draw();
          hudElementsValue.Update();
        }
      };
    }


    private async void ShowCharacterInformation(bool visible)
    {
      Debug.WriteLine("Showing character info");
      _hudElements[HudIds.CharacterInformation].Visible = visible;
    }

    private async void ShowCharacterCreationMenu(bool visible)
    {
      _menus[MenuIds.CharacterCreator].Visible = visible;
    }
  }
}