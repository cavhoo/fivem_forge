extern alias CFX;
using System;
using Client.Models;
using BaseScript = CFX::CitizenFX.Core.BaseScript;
using Debug = CFX::CitizenFX.Core.Debug;

namespace Client
{
  public class Main: BaseScript
  {
    public bool Instantiated { get; set; }
    
    public Main()
    {
      Debug.WriteLine("CityOfMindClient:Booting");
      EventHandlers[ClientEvents.ScriptStart] += new Action<string>(OnClientStart);
    }

    private void OnClientStart(string resourceName)
    {
      if (Instantiated == true)
      {
        return;
      }
      
      Instantiated = true;
    }
  }
}