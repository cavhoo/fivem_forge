
using CitizenFX.Core;

namespace Client.Utils
{
  public class Parser
  {
    public static Vector3 StringToVector3(string vectorString)
    {
      // If we have no pos return default position. TODO: Fallback to Airport :) 
      if (vectorString == null) return Vector3.Zero;
      var split = vectorString.Split(':');
      return new Vector3(float.Parse(split[0]), float.Parse(split[0]), float.Parse(split[0]));
    }
  }
}