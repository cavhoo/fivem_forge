using Newtonsoft.Json;

namespace Server.Models
{
    public enum ErrorCodes
    {
        Exists = 1000,
        NotFound = 1001,
        RankExists = 1002,
        RankNotFound = 1003,
        RankNameNotValid = 1004,
        ActionNotAllowed = 1005,
        FactionMemberNotFound = 1006,
        RankPermissionNotFound = 1007,
    }

    public enum JobErrors
    {
        Exists = 8000,
        NotFound = 8001,
        RankExists = 8002,
        RankNotFound = 8003,
        RankNameNotValid = 8004,
        ActionNotAllowed = 8005,
        JobMemberNotFound = 8006,
    }


    public class ErrorTypes
    {
        public static string DatabaseError = "DatabaseError";
        public static string AccountError = "AccountError";
        public static string NetworkError = "NetworkError";
        public static string CharacterError = "CharacterError";
        public static string GenericError = "GenericError";
        public static string AtmError = "AtmError";
        public static string BankError = "BankError";
        public static string WalletError = "WalletError";
        public static string FactionError = "FactionError";
    }

    /// <summary>
    /// Class <c>Error</c>
    /// Used to send error messages to the client.
    /// Client has a list of error codes and translations,
    /// available to make sure the user understands what went south.
    /// </summary>
    public class Error
    {
        public string Type { get; set; }
        public int Code { get; set; }

        public Error(string type, int code)
        {
            Type = type;
            Code = code;
        }


        public Error(CitizenFX.Core.Player player, string type, int code)
        {
            Type = type;
            Code = code;

            player.TriggerEvent($"CityOfMind:Error{type}", code);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
