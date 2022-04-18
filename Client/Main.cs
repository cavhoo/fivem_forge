using System;
using CitizenFX.Core;
using Client.Models;

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