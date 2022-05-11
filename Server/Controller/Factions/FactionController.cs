using System;
using System.Linq;
using CitizenFX.Core;
using Common.Models;
using Server.Controller.Base;
using Server.Models;
using Server.Models.Factions;
using Server.Models.Permissions;
using Server.Utils;
using Player = CitizenFX.Core.Player;

namespace Server.Controller.Factions
{
  /// <summary>
  /// Common Faction functions
  /// </summary>
  public class FactionController : BaseClass
  {
    public FactionController(EventHandlerDictionary handlers, Action<string, object[]> eventTriggerFunc,
      Action<Player, string, object[]> clientEventTriggerFunc, Action<string, object[]> clientEventTriggerAllFunc) : base(handlers, eventTriggerFunc,
      clientEventTriggerFunc, clientEventTriggerAllFunc)
    {
      EventHandlers[FactionEvents.RegisterFaction] += new Action<string, FactionRank[]>(OnRegisterFaction);
      EventHandlers[FactionEvents.RenameFaction] += new Action<Player, string, string>(OnRenameFaction);
      EventHandlers[FactionEvents.CreateFactionBankaccount] += new Action<string, int>(OnCreateFactionBankAccount);
      EventHandlers[FactionEvents.CreateFactionRank] += new Action<Player, string, int>(OnCreateFactionRank);
      EventHandlers[FactionEvents.FactionRankRemove] += new Action<Player, string>(OnRemoveFactionRank);
      EventHandlers[FactionEvents.RenameFactionRank] += new Action<Player, string, string>(OnRenameFactionRank);
      EventHandlers[FactionEvents.EditFactionRankPermissions] += new Action<Player, string, RankPermissions>(OnEditFactionRankPermissions);
      EventHandlers[FactionEvents.RemoveMemberFromFaction] += new Action<Player, string>(OnRemoveMemberFromFaction);
      EventHandlers[FactionEvents.SetMemberFactionRank] += new Action<Player, string, string>(OnSetMemberFactionRank);
    }
    /// <summary>
    /// Registers and creates a new faction entry inside the database.
    /// This will assign a UUID to the faction, and creates all the entries
    /// for the ranks of the faction.
    /// Will dispatch an event that the faction has been created, containing
    /// the UUID of the faction.
    /// </summary>
    /// <param name="factionName"></param>
    /// <param name="ranks"></param>
    private async void OnRegisterFaction(string factionName, FactionRank[] ranks)
    {
      var factionExists = Context.Factions.FirstOrDefault(f => f.Name == factionName);

      if (factionExists != null)
      {
        Debug.WriteLine("You can only register a faction once.");
      }

      var newFaction = new Faction()
      {
        Name = factionName,
        Uuid = Guid.NewGuid().ToString(),
        Jobs = new[] { "" }, // TODO: Figure out a way to assign jobs to factions more easily.
      };

      Context.Factions.Add(newFaction);
      await Context.SaveChangesAsync();
      TriggerEvent(FactionEvents.FactionRegistered);
    }

    private async void OnRenameFaction([FromSource] Player player, string factionId, string newName)
    {
      var faction = Context.Factions.FirstOrDefault(f => f.Uuid == factionId);

      if (faction == null)
      {
        // Should never happen!
        player.TriggerEvent("CityOfMind:FactionDoesNotExist");
        return;
      }

      faction.Name = newName;
      await Context.SaveChangesAsync();
      player.TriggerEvent(FactionEvents.FactionRenamed, faction.Uuid, faction.Name);
    }

    private async void OnCreateFactionBankAccount(string factionId, int startingBalance)
    {
      var bankAccount = Context.BankAccount.FirstOrDefault(b => b.Holder == factionId);
      if (bankAccount != null)
      {
        // If we have a bank account abort.
        return;
      }

      var newBankAccount = new BankAccount()
      {
        Holder = factionId,
        Saldo = startingBalance,
        AccountNumber = BankAccountTools.GenerateAccountNumber()
      };

      Context.BankAccount.Add(newBankAccount);
      await Context.SaveChangesAsync();
    }

    private async void OnCreateFactionRank([FromSource] Player player,string factionId, string rankName, int salary)
    {
      var rankExists = Context.FactionRanks.FirstOrDefault(r => r.Name == rankName);

      if (rankExists != null)
      {
        player.TriggerEvent(FactionEvents.FactionRankExists);
        return;
      }

      var newRank = new FactionRank()
      {
        Name = rankName,
        Salary = salary,
        FactionId = factionId,
        Uuid = Guid.NewGuid().ToString()
      };
      Context.FactionRanks.Add(newRank);
      await Context.SaveChangesAsync();
      
      player.TriggerEvent(FactionEvents.FactionRankCreated);
    }

    private async void OnRemoveFactionRank([FromSource] Player player, string rankId)
    {
      
    }

    private async void OnRenameFactionRank([FromSource] Player player, string factionRankId, string newName)
    {
      
    }

    private async void OnEditFactionRankPermissions([FromSource] Player player, string factionRankId, RankPermissions rankPermissionses)
    {
      
    }

    private async void OnRemoveMemberFromFaction([FromSource] Player player, string playerIdToRemove)
    {
      
    }

    private async void OnSetMemberFactionRank([FromSource] Player player, string playerId, string rankId)
    {
      
    }
  }
}