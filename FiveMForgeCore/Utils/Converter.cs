using CitizenFX.Core;

namespace FiveMForge.Utils
{
    public class Converter
    {
        public static Vector3 PositionStringToVector3(string position)
        {
            var split = position.Split(':');
            if (split.Length == 0) return Vector3.Zero;

            return new Vector3(
                float.Parse(split[0]),
                float.Parse(split[1]),
                float.Parse(split[2])
            );
        }
    }
}