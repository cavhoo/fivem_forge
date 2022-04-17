using CitizenFX.Core;
using Newtonsoft.Json;
using Server.Utils;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Server.Controller.Config
{
    
    public class ConfigController
    {
        private static ConfigController _instance = null;
        public Config Config { get; set; }
        private ConfigController()
        {
            Debug.WriteLine("Loading config file...");
            var ymlString = Loader.LoadConfigFile("./server.yaml");
            if (ymlString == null)
            {
                Debug.WriteLine("No config file found...");
                return;
            }
            
            var deserializer = new DeserializerBuilder().Build();

            Config = deserializer.Deserialize<Config>(ymlString);
        }

        public static ConfigController GetInstance()
        {
            return _instance ??= new ConfigController();
        }
    }
}