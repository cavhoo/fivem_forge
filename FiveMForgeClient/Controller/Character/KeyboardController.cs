extern alias CFX;
using System;
using CFX::CitizenFX.Core;
using static CFX::CitizenFX.Core.Native.API;

namespace FiveMForgeClient.Controller.Character
{
    public class KeyboardController : BaseScript
    {
        private static KeyboardController _instance;

        public event EventHandler<Control> ControlPressed;
        
        private KeyboardController()
        {
            Tick += async () =>
            {
                if (Game.IsControlPressed(0, Control.SelectCharacterMichael))
                {
                    ControlPressed?.Invoke(this, Control.SelectCharacterMichael);
                }
            };
        }
        
        public static KeyboardController GetInstance()
        {
            return _instance ??= new KeyboardController();
        }
        
        
    }
}