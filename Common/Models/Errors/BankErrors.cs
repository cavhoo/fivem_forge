namespace Common.Models.Errors
{
  public class BankErrors
  {
    public static Error AccountNotFound = new Error(ErrorTypes.BankError, BankErrorCodes.NotFound);
    public static Error NotEnoughBalance = new Error(ErrorTypes.BankError, BankErrorCodes.NotEnoughBalance);
    public static Error IncorrectPin = new Error(ErrorTypes.BankError, BankErrorCodes.IncorrectPin);
  }

  public class BankErrorCodes
  {
    public static int NotFound = 100001;
    public static int NotEnoughBalance = 100002;
    public static int IncorrectPin = 100003;
  }
}