namespace Server.Models.Errors
{
  public static class AccountErrors
  {
    public static Error NotFound = new Error(ErrorTypes.AccountError, AccountErrorCodes.AccountNotFound);
    public static Error NotCreated = new Error(ErrorTypes.AccountError, AccountErrorCodes.AccountNotCreated);
    public static Error Deleted = new Error(ErrorTypes.AccountError, AccountErrorCodes.AccountDeleted);
    public static Error Banned = new Error(ErrorTypes.AccountError, AccountErrorCodes.AccountBanned);
    public static Error NotWhitelisted = new Error(ErrorTypes.AccountError, AccountErrorCodes.AccountNotWhitelisted);
  }

  public static class AccountErrorCodes
  {
    public const int AccountNotFound = 1000;
    public const int AccountNotCreated = 1001;
    public const int AccountDeleted = 1002;
    public const int AccountNotWhitelisted = 1003;
    public const int AccountBanned = 1004;
  }
}
