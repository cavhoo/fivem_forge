extern alias CFX;
using System;
using System.Runtime.Remoting.Channels;
using CFX::CitizenFX.Core;
using CFX::CitizenFX.Core.UI;
using NativeUI;
using NativeUI.PauseMenu;
using static CFX::CitizenFX.Core.Native.API; 

namespace FiveMForgeClient.View.UI.Components
{
    public class SimpleMenu : BaseScript
    {
        private bool _initialized = false;
        private bool checkbox = false;
        private MenuPool _menuPool;
        public SimpleMenu()
        {
            
        }
        private void OnSimpleMenuStart()
        {
            if (!_initialized)
            {
                _menuPool = new MenuPool();
                var mainMenu = new UIMenu("Native UI", "~b~NativeUI Showcase", true);
                _menuPool.Add(mainMenu);

                // First menu entry
                var newItem = new UIMenuCheckboxItem("Checkbox damn!", UIMenuCheckboxStyle.Cross, checkbox,
                    "Checkboxing is cool huh?");
                mainMenu.AddItem(newItem);
                mainMenu.OnCheckboxChange += (sender, item, checked_) =>
                {
                    if (item == newItem)
                    {
                        checkbox = checked_;
                        Screen.ShowNotification("~r~Checkbox Status: ~b~" + checkbox);
                    }
                };
                
                _menuPool.RefreshIndex();

                Tick += async () =>
                {
                    _menuPool.ProcessMenus();
                    if (Game.IsControlPressed(0, Control.SelectCharacterMichael) && !_menuPool.IsAnyMenuOpen())
                    {
                        mainMenu.Visible = !mainMenu.Visible;
                    }
                };
            }
        }
    }
    
}