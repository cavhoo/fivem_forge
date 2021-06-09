namespace FiveMForge.Models
{
    public static class ServerEvents
    {
        public const string SpawnAt = "FiveMForge:SpawnAt";
        public const string GetLastSpawnPosition = "FiveMForge:GetLastSpawnPosition";
        public const string GetNearestHospital = "FiveMForge:GetNearestHospital";
        public const string LoadAtmLocations = "FiveMForge:LoadAtmLocations";
        public const string LoadBankLocations = "FiveMForge:LoadBankLocations";
        public const string AtmLocationsLoaded = "FiveMForge:AtmLocationsLoaded";
        public const string BankLocationsLoaded = "FiveMForge:BankLocationsLoaded";
        public const string BankAccountLoaded = "FiveMForge:BankAccountLoaded";
        public const string LoadBankAccount = "FiveMForge:LoadBankAccount";
        public const string LoadWallet = "FiveMForge:LoadWallet";
        public const string WalletLoaded = "FiveMForge:WalletLoaded";
        public const string SavePoiPosition = "FiveMForge:SavePOIPosition";
        public const string GetSessionId = "FiveMForge:GetSessionId";
        public const string SessionIdLoaded = "FiveMForge:SessionIdLoaded";
        public const string Heartbeat = "FiveMForge:Heartbeat";
        public const string Error = "FiveMForge:Error";
        public const string UIError = "FiveMForge:UIError";
        public const string CharacterCreated = "FiveMForge:CharacterCreated";
        public const string CharacterSaved = "FiveMForge:CharacterSaved";
        public const string CharactersLoaded = "FiveMForge:CharactersLoaded";
        public const string GoToCharacterSelection = "FiveMForge:ShowCharacterSelection";
        public const string LoadCharacters = "FiveMForge:LoadCharacters";
        public const string DisconnectPlayer = "FiveMForge:DisconnectPlayer";
    }

    public static class FiveMEvents
    {
        public const string PlayerConnecting = "playerConnecting";
        public const string PlayerDisconnecting = "playerDropped";
    }
}