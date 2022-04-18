using System;
using Client.Models;
using BaseScript = CitizenFX.Core.BaseScript;

namespace Client.Controller.State
{
  public interface IClientState
  {
    public Models.Character.Character ActiveCharacter { get; set; }
    public int WalletAmount { get; set; }
    public int BankAmount { get; set; }
    public string ActiveJob { get; set; }
    public int WantedLevel { get; set; }
  }

  public class ClientState : IClientState
  {
    public Models.Character.Character ActiveCharacter { get; set; }
    public int WalletAmount { get; set; }
    public int BankAmount { get; set; }
    public string ActiveJob { get; set; }
    public int WantedLevel { get; set; }
  }
  
  
  public class ClientStateController : BaseScript
  {
    private IClientState State;
    
    public ClientStateController()
    {
      EventHandlers[ClientEvents.ScriptStart] += new Action<string>(InitializeState);
    }

    private void InitializeState(string resourceName)
    {
      State = new ClientState();
      EventHandlers[ClientEvents.UpdateClientState] += new Action<ClientState>(PatchState);
    }

    private void PatchState(ClientState state)
    {
      State.ActiveCharacter = state.ActiveCharacter ?? State.ActiveCharacter;
      State.ActiveJob = state.ActiveJob ?? State.ActiveJob;
      State.BankAmount = state.BankAmount > Int32.MinValue ? state.BankAmount : State.BankAmount;
      State.WalletAmount = state.WalletAmount > Int32.MinValue ? state.WalletAmount : State.WalletAmount;
      State.WantedLevel = state.WantedLevel != -1 && state.WantedLevel != State.WantedLevel ? state.WantedLevel : State.WantedLevel;
      
      TriggerEvent(ClientEvents.ClientStateUpdated);
    }
  }
}