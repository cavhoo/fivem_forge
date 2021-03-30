using System;
using CitizenFX.Core;

namespace FiveMForgeClient
{
    public class BaseClass : BaseScript
    {
        public BaseClass()
        {
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
        }

        protected virtual void OnClientResourceStart(string resourceName)
        {
            Debug.WriteLine("Not Implemented...");
        }
    }
}