namespace FiveMForgeClient.Models
{
    public static class ServerEvents
    {
        public const string SpawnAt = "FiveMForge:SpawnAt";
        public const string GetLastSpawnPosition = "FiveMForge:GetLastSpawnPosition";
        public const string LoadAtmLocations = "FiveMForge:LoadAtmLocations";
        public const string LoadBankLocations = "FiveMForge:LoadBankLocations";
        public const string AtmLocationsLoaded = "FiveMForge:AtmLocationsLoaded";
        public const string BankLocationsLoaded = "FiveMForge:BankLocationsLoaded";
        public const string SavePOIPosition = "FiveMForge:SavePOIPosition";
        public const string LoadCharacters = "FiveMForge:LoadCharacters";
        public const string CharactersLoaded = "FiveMForge:CharactersLoaded";
    }

    public static class ClientEvents
    {
        public const string ScriptStart = "onClientResourceStart";
        public const string SpawnPlayer = "FiveMForge:SpawnPlayer";
        public const string OpenCharacterSelection = "FiveMForge:OpenCharacterSelection";
    }
}