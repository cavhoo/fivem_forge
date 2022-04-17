namespace Server.Models
{
    public static class ServerEvents
    {
        public const string CreatePayments = "CityOfMind:CreatePayment";
        public const string SpawnAt = "CityOfMind:SpawnAt";
        public const string AtmLocationsLoaded = "CityOfMind:AtmLocationsLoaded";
        public const string BankLocationsLoaded = "CityOfMind:BankLocationsLoaded";
        public const string BankAccountLoaded = "CityOfMind:BankAccountLoaded";
        public const string LoadWallet = "CityOfMind:LoadWallet";
        public const string WalletLoaded = "CityOfMind:WalletLoaded";
        public const string SavePoiPosition = "CityOfMind:SavePOIPosition";
        public const string SessionIdLoaded = "CityOfMind:SessionIdLoaded";
        public const string Heartbeat = "CityOfMind:Heartbeat";
        public const string Error = "CityOfMind:Error";
        public const string UiError = "CityOfMind:UIError";
        public const string CharacterSaved = "CityOfMind:CharacterSaved";
        public const string CharactersLoaded = "CityOfMind:CharactersLoaded";
        public const string GoToCharacterSelection = "CityOfMind:ShowCharacterSelection";
        public const string DisconnectPlayer = "CityOfMind:DisconnectPlayer";
        public const string AvailableCharacterCountLoaded = "CityOfMind:AvailableCharacterSlotsLoaded";
        public const string MoneyWithdrawn = "CityOfMind:MoneyWithdrawn";
        public const string MoneyWithdrawNotEnoughBalance = "CityOfMind:MoneyWithdrawNotEnoughBalance";
        public const string MoneyWithdrawIncorrectPin = "CityOfMind:MoneyWithdrawIncorrectPin";
    }

    public static class ServerClientEvents
    {
        public const string RandomCharacterDataCreated = "CityOfMind:RandomCharacterDataCreated";
    }

    public static class ClientEvents
    {
        public const string CreateBankAccountForCharacter = "CityOfMind:CreateBankAccountForCharacter";
        public const string LoadCharacters = "CityOfMind:LoadCharacters";
        public const string GetNearestHospital = "CityOfMind:GetNearestHospital";
        public const string GetLastSpawnPosition = "CityOfMind:GetLastSpawnPosition";
        public const string LoadBankLocations = "CityOfMind:LoadBankLocations";
        public const string LoadAtmLocations = "CityOfMind:LoadAtmLocations";
        public const string WithDrawMoney = "CityOfMind:WithdrawMoney";
        public const string DepositMoney = "CityOfMind:DepositMoney";
        public const string LoadAvailableCharacterCount = "CityOfMind:GetAvailableCharacterSlots";
        public const string CreateCharacter = "CityOfMind:CreateCharacter";
        public const string GetSessionId = "CityOfMind:GetSessionId";
        public const string LoadBankAccount = "CityOfMind:LoadBankAccount";
        public const string GetRandomCharacterData = "CityOfMind:GetRandomCharacterData";
    }

    public static class FiveMEvents
    {
        public const string PlayerConnecting = "playerConnecting";
        public const string PlayerDisconnecting = "playerDropped";
    }
}