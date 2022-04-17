namespace Server.Models.Errors
{
  public static class CharacterErrors
  {
      public static Error CharacterNotFound = new Error(ErrorTypes.CharacterError, CharacterErrorCodes.CharacterNotFound);

      public static Error CharacterNotSaved =
        new Error(ErrorTypes.CharacterError, CharacterErrorCodes.CharacterNotSaved);

      public static Error CharacterNotLoaded =
        new Error(ErrorTypes.CharacterError, CharacterErrorCodes.CharacterNotLoaded);

  }

  public static class CharacterErrorCodes
  {
    public const int CharacterNotFound = 1000;
    public const int CharacterNotSaved = 1001;
    public const int CharacterNotLoaded = 1002;
  }
}