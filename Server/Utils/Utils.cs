using System;

namespace Server.Utils
{
  public class Float
  {
    public static bool FloatEquals(float a, float b, float tolerance)
    {
      return Math.Abs(a - b) < tolerance;
    }
  }

  public class BankAccountTools
  {
    public static string GenerateAccountNumber()
    {
      var rand = new Random();

      var firstTriple = rand.Next(1000);
      var secondTriple = rand.Next(1000);
      var thirdTriple = rand.Next(1000);
      return $"{firstTriple}{secondTriple}{thirdTriple}";
    }
  }
}