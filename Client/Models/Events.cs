namespace Client.Models
{
    /// <summary>
    /// Events that are fired against and from the server.
    /// </summary>
    public static class ServerEvents
    {
        public const string GetLastSpawnPosition = "CityOfMind:GetLastSpawnPosition";
        public const string LoadAtmLocations = "CityOfMind:LoadAtmLocations";
        public const string LoadBankLocations = "CityOfMind:LoadBankLocations";
        public const string AtmLocationsLoaded = "CityOfMind:AtmLocationsLoaded";
        public const string BankLocationsLoaded = "CityOfMind:BankLocationsLoaded";
        public const string SavePOIPosition = "CityOfMind:SavePOIPosition";
        public const string LoadCharacters = "CityOfMind:LoadCharacters";
        public const string CharactersLoaded = "CityOfMind:CharactersLoaded";
        public const string CreateCharacter = "CityOfMind:CreateCharacter";
        public const string CharacterSaved = "CityOfMind:CharacterSaved";
        public const string GetAvailableCharacterSlots = "CityOfMind:GetAvailableCharacterSlots";
        public const string AvailableCharacterSlotsLoaded = "CityOfMind:AvailableCharacterSlotsLoaded";
        public const string LoadBankAccount = "CityOfMind:LoadBankAccount";
        public const string BankAccountLoaded = "CityOfMind:BankAccountLoaded";
        public const string WithdrawMoney = "CityOfMind:WithdrawMoney";
        public const string ShowNotification = "CityOfMind:ShowNotification";
    }

    /// <summary>
    /// Events that are used only client side.
    /// </summary>
    public static class ClientEvents
    {
        /// <summary>
        /// General Client Side Events
        /// </summary>
        public const string SpawnAt = "CityOfMind:SpawnAt";
        public const string PlayerSpawned = "CityOfMind:PlayerSpawned";
        public const string ScriptStart = "onClientResourceStart";
        public const string SpawnPlayer = "CityOfMind:SpawnPlayer";
        public const string ZoomOntoFace = "CityOfMind:ZoomOntoFace";
        public const string ZoomBody = "CityOfMind:ZoomBody";
        
        /// <summary>
        /// UI Events
        /// </summary>
        public const string ShowCharacterInformation = "CityOfMind:ShowCharacterInformation";
        public const string ShowCharacterCreationMenu = "CityOfMind:ShowCharacterCreationMenu";
        public const string CharacterCreationClosed = "CityOfMind:CharacterCreationClosed";
        public const string UpdateCharacterModel = "CityOfMind:UpdateCharacterModel";

        /// <summary>
        /// State Events
        /// </summary>
        public const string UpdateClientState = "CityOfMind:UpdateClientState";
        public const string ClientStateUpdated = "CityOfMind:ClientStateUpdated";

    }
    
    
}