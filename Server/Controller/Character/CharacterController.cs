using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Server.Controller.Base;
using Server.Controller.Config;
using Server.Models;
using Server.Models.Character;
using Server.Models.Errors;
using Player = CitizenFX.Core.Player;

namespace Server.Controller.Character
{
  public class CharacterController : BaseClass
  {
    private Vector3 _airportSpawnLocation = new(-1046.6901f, -2770.3647f, 4.62854f);

    public CharacterController(EventHandlerDictionary handlers, Action<string, object[]> eventTriggerFunc,
      Action<Player, string, object[]> clientEventTriggerFunc) : base(handlers, eventTriggerFunc,
      clientEventTriggerFunc)
    {
      EventHandlers[ClientEvents.GetRandomCharacterData] += new Action<Player>(OnGetRandomCharacterData);
      EventHandlers[ClientEvents.CreateCharacter] += new Action<Player, string>(OnCreateCharacter);
      EventHandlers[ClientEvents.LoadCharacters] += new Action<Player>(OnLoadCharacters);
    }

    private void OnGetRandomCharacterData([FromSource] Player player)
    {
      var rng = new Random();
      var playIdentifier = API.GetPlayerIdentifier(player.Handle, 0);

      var newCharacter = new Models.Character.Character();
      newCharacter.Mom = rng.Next(0, CharacterComponents.InheritanceMoms.Length);


      newCharacter.Dad = rng.Next(0, CharacterComponents.InheritanceDads.Length);

      int randomGender = rng.Next(0, 10);
      newCharacter.Gender = randomGender >= 5 ? "male" : "female";
      // Set nose data

      newCharacter.NoseWidth = (float)rng.NextDouble();
      newCharacter.NoseTipLength = (float)rng.NextDouble();
      newCharacter.NoseTipLowering = (float)rng.NextDouble();
      newCharacter.NoseBoneBend = (float)rng.NextDouble();
      newCharacter.NoseBoneOffset = (float)rng.NextDouble();
      newCharacter.NoseTipHeight = (float)rng.NextDouble();

      newCharacter.EyeColor = rng.Next(0, CharacterComponents.EyeColor.Length);
      newCharacter.EyeBrowHeight = (float)rng.NextDouble();
      newCharacter.EyeBrowBulkiness = (float)rng.NextDouble();
      newCharacter.EyeBrowStyle = rng.Next(0, CharacterComponents.EyebrowStyles.Length);
      newCharacter.EyeBrowColor = rng.Next(0, 10);

      newCharacter.CheekBoneWidth = (float)rng.NextDouble();
      newCharacter.CheekWidth = (float)rng.NextDouble();
      newCharacter.CheekBoneHeight = (float)rng.NextDouble();

      newCharacter.LipThickness = (float)rng.NextDouble();

      if (newCharacter.Gender == "female")
      {
        newCharacter.LipStickVariant = rng.Next(0, CharacterComponents.LipStick.Length);
        newCharacter.LipStickColor = rng.Next(0, CharacterComponents.MakeUp.Length);
        newCharacter.BlushColor = rng.Next(0, CharacterComponents.MakeUp.Length);
        newCharacter.BlushVariant = rng.Next(0, CharacterComponents.Blush.Length);

        newCharacter.MakeUpVariant = rng.Next(0, 5);
        newCharacter.MakeUpColor = rng.Next(0, CharacterComponents.MakeUp.Length);
      }


      newCharacter.HairStyle = newCharacter.Gender == "male"
        ? rng.Next(0, CharacterComponents.MaleHairStyles.Length)
        : rng.Next(0, CharacterComponents.FemaleHairStyles.Length);
      newCharacter.HairColor = rng.Next(0, 10);
      newCharacter.HairHighlightColor = rng.Next(0, 10);

      if (newCharacter.Gender == "male")
      {
        newCharacter.ChestHairColor = rng.Next(0, 10);
        newCharacter.ChestHairVariant = rng.Next(0, CharacterComponents.ChestHair.Length);
        newCharacter.BeardVariant = rng.Next(0, CharacterComponents.BeardStyles.Length);
        newCharacter.BeardColor = rng.Next(0, 10);
      }
      
      player.TriggerEvent(ServerClientEvents.RandomCharacterDataCreated, newCharacter);
    }

    /// <summary>
    /// Saves the created character in the database to reload it later when the player selects it.
    /// </summary>
    /// <param name="player"></param>
    private async void OnCreateCharacter([FromSource] Player player, string characterData)
    {
      var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);
      var playerAccount = Context.Players.FirstOrDefault(p => p.AccountId == playerIdentifier);

      if (playerAccount == null)
      {
        player.TriggerEvent(ServerEvents.Error, AccountErrors.NotFound.ToString());
        return;
      }

      var data = JsonConvert.DeserializeObject<IDictionary<string, object>>(characterData);
      var newCharacter = new Models.Character.Character();
      newCharacter.Birthdate = Convert.ToString(data["Birthdate"]);
      newCharacter.Firstname = Convert.ToString(data["Firstname"]);
      newCharacter.Lastname = Convert.ToString(data["Lastname"]);
      newCharacter.Mom = Convert.ToInt32(data["Mom"]);
      newCharacter.Dad = Convert.ToInt32(data["Dad"]);
      newCharacter.Gender = Convert.ToString(data["Gender"]);
      // Set nose data
      newCharacter.NoseWidth = Convert.ToSingle(data["NoseWidth"]);
      newCharacter.NoseTipLength = Convert.ToSingle(data["NoseLength"]);
      newCharacter.NoseTipLowering = Convert.ToSingle(data["NoseTipLowering"]);
      newCharacter.NoseBoneBend = Convert.ToSingle(data["NoseBoneBend"]);
      newCharacter.NoseBoneOffset = Convert.ToSingle(data["NoseBoneOffset"]);
      newCharacter.NoseTipHeight = Convert.ToSingle(data["NoseHeight"]);

      newCharacter.EyeColor = Convert.ToInt32(data["EyeColor"]);
      newCharacter.EyeBrowHeight = Convert.ToSingle(data["EyebrowHeight"]);
      newCharacter.EyeBrowBulkiness = Convert.ToSingle(data["EyebrowBulkiness"]);
      newCharacter.EyeBrowStyle = Convert.ToInt32(data["EyebrowShape"]);
      newCharacter.EyeBrowColor = Convert.ToInt32(data["EyebrowColor"]);

      newCharacter.CheekBoneWidth = Convert.ToSingle(data["CheekBoneWidth"]);
      newCharacter.CheekWidth = Convert.ToSingle(data["CheekWidth"]);
      newCharacter.CheekBoneHeight = Convert.ToSingle(data["CheekBoneHeight"]);

      newCharacter.LipThickness = Convert.ToSingle(data["LipsThickness"]);
      newCharacter.LipStickVariant = Convert.ToInt32(data["LipstickVariant"]);
      newCharacter.LipStickColor = Convert.ToInt32(data["LipstickColor"]);
      newCharacter.BlushColor = Convert.ToInt32(data["BlushColor"]);
      newCharacter.BlushVariant = Convert.ToInt32(data["BlushVariant"]);

      newCharacter.MakeUpVariant = Convert.ToInt32(data["MakeUpVariant"]);
      newCharacter.MakeUpColor = Convert.ToInt32(data["MakeUpColor"]);

      newCharacter.HairStyle = Convert.ToInt32(data["HairShape"]);
      newCharacter.HairColor = Convert.ToInt32(data["HairColor"]);
      newCharacter.HairHighlightColor = Convert.ToInt32(data["HairHighlightColor"]);
      newCharacter.ChestHairColor = Convert.ToInt32(data["ChestHairColor"]);
      newCharacter.ChestHairVariant = Convert.ToInt32(data["ChestHairShape"]);
      newCharacter.BeardVariant = Convert.ToInt32(data["BeardShape"]);
      newCharacter.BeardColor = Convert.ToInt32(data["BeardColor"]);

      // var clothingData = (IDictionary<string, object>)data["clothes"];
      //
      // newCharacter.Hat = Convert.ToInt32(clothingData["Hat"]);
      // newCharacter.Shoes = Convert.ToInt32(clothingData["Shoes"]);
      // newCharacter.Jacket = Convert.ToInt32(clothingData["Jacket"]);
      // newCharacter.Shirt = Convert.ToInt32(clothingData["Shirt"]);
      // newCharacter.Glasses = Convert.ToInt32(clothingData["Glasses"]);
      // newCharacter.Mask = Convert.ToInt32(clothingData["Mask"]);
      // newCharacter.Pants = Convert.ToInt32(clothingData["Pants"]);
      //

      newCharacter.AccountUuid = playerAccount.AccountUuid;
      newCharacter.CharacterUuid = Guid.NewGuid().ToString();
      newCharacter.LastPos = $"{_airportSpawnLocation.X}:{_airportSpawnLocation.Y}:{_airportSpawnLocation.Z}";

      Context.Characters.Add(newCharacter);

      var rand = new Random();

      var firstTriple = rand.Next(1000);
      var secondTriple = rand.Next(1000);
      var thirdTriple = rand.Next(1000);
      var accountNumber = $"{firstTriple}{secondTriple}{thirdTriple}";

      var newBankAccount = new BankAccount
      {
        Holder = newCharacter.CharacterUuid,
        Saldo = ConfigController.GetInstance().Config.InitialBankAmountBalance,
        AccountNumber = accountNumber
      };
      Context.BankAccount.Add(newBankAccount);

      await Context.SaveChangesAsync();

      player.TriggerEvent(ServerEvents.CharacterSaved, newCharacter.CharacterUuid);
    }

    /// <summary>
    ///  Reloads all characters that  are available to a player.
    /// </summary>
    /// <param name="player"></param>
    private void OnLoadCharacters([FromSource] Player player)
    {
      Debug.WriteLine("Loading characters");
      var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);
      var playerAccount = Context.Players.FirstOrDefault(p => p.AccountId == playerIdentifier);

      if (playerAccount == null) return;

      var characters = Context.Characters.Where(c => c.AccountUuid == playerAccount.AccountUuid).ToList();

      player.TriggerEvent(ServerEvents.CharactersLoaded, JsonConvert.SerializeObject(characters));
    }
  }
}