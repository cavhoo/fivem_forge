namespace Common.Models.Errors
{
  public class WalletErrors
  {
    public static Error NotEnoughBalance = new Error(ErrorTypes.WalletError, WalletErrorCodes.NotEnoughBalance);
    public static Error NotFound = new Error(ErrorTypes.WalletError, WalletErrorCodes.NotFound);
  }

  public class WalletErrorCodes
  {
    public static int NotEnoughBalance = 400001;
    public static int NotFound = 400002;
  }
}