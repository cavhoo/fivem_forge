using System;
using System.Linq;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FiveMForge.Controller.Base;
using FiveMForge.Models;
using FiveMForge.Models.Errors;
using Newtonsoft.Json;
using Player = CitizenFX.Core.Player;

namespace FiveMForge.Controller.Character
{
    public class CharacterController : BaseClass
    {
        public CharacterController()
        {
            EventHandlers[ServerEvents.CharacterCreated] += new Action<Player>(OnCharacterCreated);
            EventHandlers[ServerEvents.LoadCharacters] += new Action<Player>(OnLoadCharacters);
        }
        /// <summary>
        /// Saves the created character in the database to reload it later when the player selects it.
        /// </summary>
        /// <param name="player"></param>
        private async void OnCharacterCreated([FromSource] Player player)
        {
            var playerIdentifier = API.GetPlayerIdentifier(player.Handle, 0);
            var playerAccount = Context.Players.FirstOrDefault(p => p.AccountId == playerIdentifier);

            if (playerAccount == null)
            {
                player.TriggerEvent(ServerEvents.Error, AccountErrors.NotFound.ToString());
                return;
            }
            
            var newCharacter = new Models.Character();
            newCharacter.AccountUuid = playerAccount.AccountUuid;
            newCharacter.AccountUuid = Guid.NewGuid().ToString();
            // TODO: Set default spawn point to Airport.

            Context.Characters.Add(newCharacter);

            await Context.SaveChangesAsync();
            
            player.TriggerEvent(ServerEvents.CharacterSaved);
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