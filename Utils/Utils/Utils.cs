using System;

namespace CityOfMindUtils.Utils
{
    public class Float
    {
        public static bool FloatEquals(float a, float b, float tolerance)
        {
            return Math.Abs(a - b) < tolerance;
        }
    }
}