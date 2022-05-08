using System;
using System.IO;
using CitizenFX.Core;

namespace Server.Utils
{
    public static class Loader
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
                throw new Exception(ex.Message);
            }
        }
    }
}