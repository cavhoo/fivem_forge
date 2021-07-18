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
using FiveMForgeClient.Services.Language;
using FiveMForgeClient.Models;
using FiveMForgeClient.View.UI.Hud;
using FiveMForgeClient.View.UI.Menu.CharacterCreate;
using FiveMForgeClient.View.UI.Menu;
using LemonUI;
using LemonUI.Menus;

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
    private ObjectPool _pool;
    private Dictionary<MenuIds, NativeMenu> _menus;
    private Dictionary<HudIds, Base> _hudElements;

    public UIMain()
    {
      _menus = new Dictionary<MenuIds, NativeMenu>();
      EventHandlers[ClientEvents.ScriptStart] += new Action<string>(CreateUi);
      //EventHandlers[ClientEvents.ShowCharacterInformation] += new Action<bool>(ShowCharacterInformation);
      EventHandlers[ClientEvents.ShowCharacterCreationMenu] += new Action<bool, int>(ShowCharacterCreationMenu);
    }

    private void CreateUi(string resourceName)
    {
      if (_initialized) return;
      _hudElements = new Dictionary<HudIds, Base>();
      _menus = new Dictionary<MenuIds, NativeMenu>();
      _initialized = true;
      _menus.Add(MenuIds.CarSpawner, new CarSpawnMenu("Car Spawner", "Spawn a car"));
      _pool = new ObjectPool();

      var characterCreator = new CharacterCreator(LanguageService.Translate("character_creator"),
        LanguageService.Translate("character_creator_description"));
      characterCreator.SetMenuPool(ref _pool);
      _menus.Add(MenuIds.CharacterCreator, characterCreator);
      
      foreach (var value in _menus.Values)
      {
        _pool.Add(value);
      }

      Tick += async () =>
      {
        _pool.Process();
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

    private async void ShowCharacterCreationMenu(bool visible, int pedId)
    {
      ((CharacterCreator)_menus[MenuIds.CharacterCreator]).SetPedId(pedId);
      _menus[MenuIds.CharacterCreator].Visible = visible;
      
    }
  }
}