extern alias CFX;
using System;
using System.Collections.Generic;
using CFX::CitizenFX.Core;
using CityOfMindClient.Models;
using FiveMForgeClient.Services.Language;
using FiveMForgeClient.View.UI.Hud;
using FiveMForgeClient.View.UI.Menu;
using LemonUI;
using LemonUI.Menus;
using Newtonsoft.Json;

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
      //EventHandlers[ClientEvents.ShowCharacterCreationMenu] += new Action<bool, int>(ShowCharacterCreationMenu);
    }

    private void CreateUi(string resourceName)
    {
      if (_initialized) return;
      _hudElements = new Dictionary<HudIds, Base>();
      _menus = new Dictionary<MenuIds, NativeMenu>();
      _initialized = true;
      _menus.Add(MenuIds.CarSpawner, new CarSpawnMenu("Car Spawner", "Spawn a car"));
      _pool = new ObjectPool();
      
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
  }
}