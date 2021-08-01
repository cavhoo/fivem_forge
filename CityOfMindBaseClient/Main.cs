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
    private CharacterSelectionController _characterSelectionController;
    private SpawnController _spawnController;
    private AtmController _atmController;
    private BankingController _bankingController;
    private DensityController _densityController;

    public Main()
    {
      EventHandlers[ClientEvents.ScriptStart] += new Action<string>(OnClientStart);
    }

    private void OnClientStart(string resourceName)
    {
      if (Instantiated == true)
      {
        return;
      }
      
      Instantiated = true;
      _characterSelectionController = new CharacterSelectionController();
      _spawnController = new SpawnController();
      _atmController = new AtmController();
      _bankingController = new BankingController();
      _densityController = new DensityController();
    }
  }
}