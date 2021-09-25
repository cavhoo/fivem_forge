namespace FiveMForge.Models.Errors
{
  public class AtmErrors
  {
    public static Error NoEnoughBalance = new Error(ErrorTypes.AtmError, AtmErrorCodes.NotEnoughBalance);
    public static Error IncorrectPin = new Error(ErrorTypes.AtmError, AtmErrorCodes.IncorrectPin);
    public static Error AccountNotFound = new Error(ErrorTypes.AtmError, AtmErrorCodes.AccountNotFound);
  }

  public static class AtmErrorCodes
  {
    public static int NotEnoughBalance = 200001;
    public static int IncorrectPin = 200002;
    public static int AccountNotFound = 200003;
  }
}