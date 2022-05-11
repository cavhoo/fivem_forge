using System.Text.RegularExpressions;

namespace Common.Models
{
    public static class Event
    {
        public static string CreateEvent(params string[] list)
        {
            var evnt = "";
            foreach (string part in list)
            {
                evnt += part;
            }
            return evnt;
        }
    }
    public static class EventActions
    {
        public const string Load = ".load";
        public const string Loaded = ".loaded";
        public const string Create = ".create";
        public const string Created = ".created";
        public const string Register = ".register";
        public const string Registered = ".registered";
        public const string Add = ".add";
        public const string Added = ".added";
        public const string Remove = ".remove";
        public const string Removed = ".removed";
    }
    
    
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
        public const string UIError = "CityOfMind:UIError";
        public const string CharacterSaved = "CityOfMind:CharacterSaved";
        public const string CharactersLoaded = "CityOfMind:CharactersLoaded";
        public const string GoToCharacterSelection = "CityOfMind:ShowCharacterSelection";
        public const string DisconnectPlayer = "CityOfMind:DisconnectPlayer";
        public const string AvailableCharacterCountLoaded = "CityOfMind:AvailableCharacterSlotsLoaded";
        public const string MoneyWithdrawn = "CityOfMind:MoneyWithdrawn";
        public const string MoneyWithdrawNotEnoughBalance = "CityOfMind:MoneyWithdrawNotEnoughBalance";
        public const string MoneyWithdrawIncorrectPin = "CityOfMind:MoneyWithdrawIncorrectPin";
    }

    public static class ModuleEvents
    {
        public const string RegisterModule = "CityOfMind:RegisterModule";
    }

    public static class JobEvents
    {
        public const string RegisterJob = "CityOfMind:RegisterJob";
        public const string JobRegistered = "CityOfMind:JobRegistered";
        public const string JobAlreadyExists = "CityOfMind:JobAlreadyExists";
        public const string SetEmployeeRank = "CityOfMind:SetEmployeeRank";
        public const string HireEmployee = "CityOfMind:HireEmployee";
        public const string EmployeeHired = "CityOfMind:EmployeeHired";
        public const string FireEmployee = "CityOfMind:FireEmployee";
        public const string EmployeeFired = "CityOfMind:EmployeeFired";
        public const string RenameRank = "CityOfMind:RenameJobRank";
        public const string RankRenamed = "CityOfMind:RankRenamed";
        public const string CreateRank = "CityOfMind:CreateJobRank";
        public const string RemoveRank = "CityOfMind:RemoveJobRank";
        public const string RankAlreadyExists = "CityOfMind:RankAlreadyExists";
        public const string RankDoesNotExist = "CityOfMind:RankDoesNotExist";
        public const string RankInUse = "CityOfMind:RankInUse";
        public const string RankCreated = "CityOfMind:RankCreated";
        public const string EditRankPermission = "CityOfMind:EditRankPemission";
        public const string GetEmployees = "CityOfMind:GetEmployees";
    }

    public static class FactionEvents
    {
        public const string RegisterFaction = "CityOfMind:RegisterFaction";
        public const string FactionRegistered = "CityOfMind:FactionRegistered";
        
        public const string RenameFaction = "CityOfMind:RenameFaction";
        public const string FactionRenamed = "CityOfMind:FactionRenamed";
        
        public const string CreateFactionBankaccount = "CityOfMind:CreateFactionBankAccount";
        public const string FactionBankaccountCreated = "CityOfMind:FactionBankaccountCreated";
        public const string SetMemberFactionRank = "CityOfMind:SetMemberFactionRank";
        public const string AddMemberToFaction = "CityOfMind:AddMemberToFaction";
        public const string RemoveMemberFromFaction = "CityOfMind:RemoveMemberFromFaction";
        public const string CreateFactionRank = "CityOfMind:CreateFactionRank";
        public const string FactionRankExists = "CityOfMind:FactionRankExists";
        public const string FactionRankCreated = "CityOfMind:FactionRankCreated";
        public const string FactionRankRemove = "CityOfMind:FactionRankRemove";
        public const string FactionRankRemoved = "CityOfMind:FactionRankRemoved";
        public const string RenameFactionRank = "CityOfMind:RenameFactionRank";
        public const string EditFactionRankPermissions = "CityOfMind:EditFactionRankPermissions";
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