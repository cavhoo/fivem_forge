using CitizenFX.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FiveMForge.Controller.Config
{
    
    public class ConfigController
    {
        private static ConfigController _instance = null;
        public string ConnectionString { get; }
        private ConfigController()
        {
            Debug.WriteLine("Loading config file...");
            var ymlString = Loader.LoadConfigFile("./server.yaml");
            if (ymlString == null)
            {
                Debug.WriteLine("No config file found...");
                return;
            }
            
            var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();
            var config = deserializer.Deserialize<Config>(ymlString);

            ConnectionString = config.ConnectionString;
        }

        public static ConfigController GetInstance()
        {
            return _instance ??= new ConfigController();
        }
    }
}