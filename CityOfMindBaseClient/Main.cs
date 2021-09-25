extern alias CFX;
using System;
using CFX::CitizenFX.Core;
using CityOfMindClient.Controller.Character;
using CityOfMindClient.Controller.Environment;
using CityOfMindClient.Controller.Money;
using CityOfMindClient.Controller.Spawn;
using CityOfMindClient.Models;

namespace CityOfMindClient
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