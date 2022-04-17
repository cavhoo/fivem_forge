using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.CompilerServices;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Server.Controller.Base;
using Server.Models;
using Player = CitizenFX.Core.Player;

namespace Server.Controller.Money
{
  /// <summary>
  /// Class <c>BankingController</c>
  /// Controls the interactions when accessing Bank related stuff
  /// inside the client, from the phone or from a teller.
  /// Also send the location information about the Banks to the client.
  /// </summary>
  public class BankingController : BaseClass
  {
    private int[] WithdrawableAmounts = new[] { 500, 1000, 2500, 5000, 10000, 50000, 100000 };

    public BankingController(EventHandlerDictionary handlers, Action<string, object[]> eventTriggerFunc,
                                   Action<Player, string, object[]> clientEventTriggerFunc): base(handlers, eventTriggerFunc, clientEventTriggerFunc)
    {
      EventHandlers[ClientEvents.LoadBankLocations] += new Action<Player>(OnBankLocationsRequested);
      EventHandlers[ClientEvents.LoadBankAccount] += new Action<Player>(OnRequestBankAccount);
      EventHandlers[ServerEvents.LoadWallet] += new Action<Player>(OnRequestWallet);
      Debug.WriteLine("Started BankingController");
    }

    private void OnBankLocationsRequested([FromSource] Player player)
    {
      var banks = Context.Banks.Where(b => b.IsActive).ToList();
      var bankListDto = new List<dynamic>();

      foreach (var bank in banks)
      {
        var locSplit = bank.Location.Split(':');
        var bankInfo = new BankInformation();
        bankInfo.Name = bank.Name;
        bankInfo.IsActive = bank.IsActive;
        bankInfo.SpriteId = 88;
        bankInfo.IsAdminOnly = bank.IsAdminOnly;
        bankInfo.X = float.Parse(locSplit[0]);
        bankInfo.Y = float.Parse(locSplit[1]);
        bankInfo.Z = float.Parse(locSplit[2]);
        bankListDto.Add(bankInfo);
      }

      player.TriggerEvent(ServerEvents.BankLocationsLoaded, JsonConvert.SerializeObject(bankListDto.ToArray()));
    }

    private void OnRequestBankAccount([FromSource] Player player)
    {
      var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);
      var currentPlayer = Context.Players.FirstOrDefault(p => p.AccountId == playerIdentifier);

      if (currentPlayer == null) return;


      var character = Context.Characters.FirstOrDefault(c => c.AccountUuid == currentPlayer.AccountUuid && c.InUse);
      if (character == null) return;

      var bankAccount = Context.BankAccount.FirstOrDefault(b => b.Holder == character.CharacterUuid);
      if (bankAccount == null) return;
      player.TriggerEvent(ServerEvents.BankAccountLoaded, JsonConvert.SerializeObject(new
      {
        bankAccount.Saldo,
        bankAccount.AccountNumber,
        AccountOwner = $"{character.Firstname} {character.Lastname}",
        WithdrawableAmounts
      }));
    }

    private void OnRequestWallet([FromSource] Player player)
    {
      // TODO: Refactor this to use the character UUID instead. So we have multiple wallets.
      var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);
      var currentPlayer = Context.Players.FirstOrDefault(p => p.AccountId == playerIdentifier);

      if (currentPlayer == null) return;

      var activeCharacter =
        Context.Characters.FirstOrDefault(c => c.AccountUuid == currentPlayer.AccountUuid && c.InUse);

      if (activeCharacter == null) return;
      var wallet = Context.Wallets.FirstOrDefault(w => w.Holder == activeCharacter.AccountUuid);

      if (wallet == null) return;

      player.TriggerEvent(ServerEvents.WalletLoaded, wallet.Saldo);
    }
  }
}