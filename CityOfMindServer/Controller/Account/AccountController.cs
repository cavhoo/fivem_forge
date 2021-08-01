using System;
using System.Linq;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FiveMForge.Controller.Base;
using FiveMForge.Models;
using Player = CitizenFX.Core.Player;

namespace FiveMForge.Controller.Account
{
  public class AccountController : BaseClass
  {
    public AccountController()
    {
      EventHandlers[ServerEvents.LoadAvailableCharacterCount] += new Action<Player>(OnLoadAvailableCharacterCount);
    }

    private void OnLoadAvailableCharacterCount([FromSource] Player player)
    {
      var accountId = API.GetPlayerIdentifier(player.Handle, 0);
      var activePlayer = Context.Players.FirstOrDefault(p => p.AccountId == accountId);
      if (activePlayer != null)
      {
        // Get active tier for this account.
        var activeTier = Context.Tiers.FirstOrDefault(t => t.Tier == activePlayer.Tier);
        Debug.WriteLine($"Found {activeTier?.Tier.ToString()} with {activeTier?.CharacterSlots} slot available");
        player.TriggerEvent(ServerEvents.AvailableCharacterCountLoaded, activeTier?.CharacterSlots);
      }
      else
      {
        // If we can't load the tier then just send one character slot!
        player.TriggerEvent(ServerEvents.AvailableCharacterCountLoaded, 1);
      }
    }
  }
}