using System;
using CitizenFX.Core;
using Common.Models;
using Server.Controller.Base;
using Server.Models.Factions;

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
      EventHandlers[FactionEvents.RegisterFaction] += new Action<string>(OnRegisterFaction);
      EventHandlers[FactionEvents.RenameFaction] += new Action<Player, string, string>(OnRenameFaction);
      EventHandlers[FactionEvents.CreateFactionBankaccount] += new Action<string, int>(OnCreateFactionBankAccount);
      EventHandlers[FactionEvents.CreateFactionRank] += new Action<Player, string, int>(OnCreateFactionRank);
      EventHandlers[FactionEvents.RemoveFactionRank] += new Action<Player, string>(OnRemoveFactionRank);
      EventHandlers[FactionEvents.RenameFactionRank] += new Action<Player, string, string>(OnRenameFactionRank);
      EventHandlers[FactionEvents.EditFactionRankPermissions] += new Action<Player, string, FactionRankPermissions>(OnEditFactionRankPermissions);
      EventHandlers[FactionEvents.RemoveMemberFromFaction] += new Action<Player, string>(OnRemoveMemberFromFaction);
      EventHandlers[FactionEvents.SetMemberFactionRank] += new Action<Player, string, string>(OnSetMemberFactionRank);
    }

    private async void OnRegisterFaction(string factionName)
    {
      
    }

    private async void OnRenameFaction([FromSource] Player player, string factionId, string newName)
    {
      
    }

    private async void OnCreateFactionBankAccount(string factionId, int startingBalance)
    {
      
    }

    private async void OnCreateFactionRank([FromSource] Player player, string rankName, int salary)
    {
      
    }

    private async void OnRemoveFactionRank([FromSource] Player player, string rankId)
    {
      
    }

    private async void OnRenameFactionRank([FromSource] Player player, string factionRankId, string newName)
    {
      
    }

    private async void OnEditFactionRankPermissions([FromSource] Player player, string factionRankId, FactionRankPermissions rankPermissionses)
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