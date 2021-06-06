using Newtonsoft.Json;
using Npgsql.TypeHandlers;

namespace FiveMForge.Models
{
    public class ErrorTypes
    {
        public static string DatabaseError = "DatabaseError";
        public static string AccountError = "AccountError";
        public static string NetworkError = "NetworkError";
        public static string CharacterError = "CharacterError";
        public static string GenericError = "GenericError";
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

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }


    public class Errors
    {
        public static string CharacterCreationError =
            new Error(ErrorTypes.DatabaseError, 1000).ToString();
    }
}