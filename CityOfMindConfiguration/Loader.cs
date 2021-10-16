using System.IO;

namespace CityOfMindConfiguration
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
                return null;
            }
        }
    }
}