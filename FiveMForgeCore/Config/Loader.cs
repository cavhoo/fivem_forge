using System.IO;
using System.Net.NetworkInformation;
using CitizenFX.Core;

namespace FiveMForge.Config
{
    public class Loader
    {
        public static string LoadConfigFile(string configPath)
        {
            try
            {
                var configContent = File.ReadAllText(configPath);
                return configContent;
            }
            catch (IOException ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}