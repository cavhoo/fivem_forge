extern alias CFX;
using System;
using System.Collections.Generic;
using System.Globalization;
using CFX::CitizenFX.Core;
using CFX::CitizenFX.Core.UI;
using CFX::System.Drawing;
using FiveMForgeClient.Controller.Character;
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
  }


  public class UIMain : BaseScript
  {
    private bool _initialized = false;
    private KeyboardController _keyboardController;
    private MenuPool _pool;
    private Dictionary<MenuIds, UIMenu> _menus;
    private List<Base> _hudElements = new List<Base>();

    public UIMain()
    {
      _menus = new Dictionary<MenuIds, UIMenu>();
      EventHandlers[ClientEvents.ScriptStart] += new Action<string>(CreateUi);
    }

    private void CreateUi(string resourceName)
    {
      if (_initialized) return;
      _keyboardController = KeyboardController.GetInstance();
      _keyboardController.ControlPressed += OnKeyPressed;
      _initialized = true;


      _hudElements.Add(new Speedometer());


      _pool = new MenuPool();
      _pool.Add(CreateSimpleMenu());
      _pool.RefreshIndex();

      _menus.Add(MenuIds.CarSpawner, new CarSpawnMenu("Car Spawner", "Spawn a car"));

      Tick += async () =>
      {
        _pool.ProcessMenus();
        _hudElements.ForEach(element => element.Draw());
      };

      Tick += async () => { _hudElements.ForEach(element => element.Update()); };
    }

    private UIMenu CreateSimpleMenu()
    {
      var menu = new UIMenu("Native UI", "~b~NativeUI Showcase", true);

      return menu;
    }

    private void OnKeyPressed(object sender, Control control)
    {
      switch (control)
      {
        case Control.SelectCharacterFranklin:
          break;
        case Control.SelectCharacterMichael:
          _menus[MenuIds.CarSpawner].Visible = !_menus[MenuIds.CarSpawner].Visible;
          break;
        case Control.SelectCharacterTrevor:
          break;
        default:
          TriggerEvent("chat:addMessage", new
          {
            color = new[] {255, 255, 255},
            args = new[] {"Unmapped command."}
          });
          break;
      }
    }
  }
}