namespace FiveMForgeClient.Models
{
    /// <summary>
    /// Events that are fired against and from the server.
    /// </summary>
    public static class ServerEvents
    {
        public const string GetLastSpawnPosition = "FiveMForge:GetLastSpawnPosition";
        public const string LoadAtmLocations = "FiveMForge:LoadAtmLocations";
        public const string LoadBankLocations = "FiveMForge:LoadBankLocations";
        public const string AtmLocationsLoaded = "FiveMForge:AtmLocationsLoaded";
        public const string BankLocationsLoaded = "FiveMForge:BankLocationsLoaded";
        public const string SavePOIPosition = "FiveMForge:SavePOIPosition";
        public const string LoadCharacters = "FiveMForge:LoadCharacters";
        public const string CharactersLoaded = "FiveMForge:CharactersLoaded";
        public const string SaveCreatedCharacter = "FiveMForge:SaveCreatedCharacter";
    }

    /// <summary>
    /// Events that are used only client side.
    /// </summary>
    public static class ClientEvents
    {
        /// <summary>
        /// General Client Side Events
        /// </summary>
        public const string SpawnAt = "FiveMForge:SpawnAt";
        public const string ScriptStart = "onClientResourceStart";
        public const string SpawnPlayer = "FiveMForge:SpawnPlayer";
        public const string ZoomOntoFace = "FiveMForge:ZoomOntoFace";
        public const string ZoomBody = "FiveMForge:ZoomBody";
        
        /// <summary>
        /// UI Events
        /// </summary>
        public const string ShowCharacterInformation = "FiveMForge:ShowCharacterInformation";
        public const string ShowCharacterCreationMenu = "FiveMForge:ShowCharacterCreationMenu";
        public const string CharacterCreationClosed = "FiveMForge:CharacterCreationClosed";
    }
    
    
}