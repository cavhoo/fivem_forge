extern alias CFX;
using System;
using System.Collections.Generic;
using System.Globalization;
using CFX::CitizenFX.Core;
using CFX::CitizenFX.Core.UI;
using CFX::System.Drawing;
using FiveMForgeClient.Controller.Character;
using FiveMForgeClient.Models;
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

        private Text _speed;
        
        public UIMain()
        {
            _menus = new Dictionary<MenuIds, UIMenu>();
            _speed = new Text("0", new PointF(50, 50), 1.0f);
           EventHandlers[ClientEvents.ScriptStart] += new Action<string>(CreateUi);
        }

        private void CreateUi(string resourceName)
        {
            if (_initialized) return;
            _keyboardController = KeyboardController.GetInstance();
            _keyboardController.ControlPressed += OnKeyPressed;
            _initialized = true;
            _pool = new MenuPool();
            _pool.Add(CreateSimpleMenu());
            _pool.RefreshIndex();
            
            _menus.Add(MenuIds.CarSpawner, new CarSpawnMenu("Car Spawner", "Spawn a car"));

            Tick += async () =>
            {
                _pool.ProcessMenus();
                if (Game.PlayerPed.IsInVehicle())
                {
                    _speed.Caption = (Game.PlayerPed.CurrentVehicle.Speed * 3.2).ToString(CultureInfo.CurrentCulture);
                    _speed.Draw();
                }   
            };
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