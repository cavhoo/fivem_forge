using CitizenFX.Core;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FiveMForge.Config
{
    
    public class ConfigController
    {
        private static ConfigController _instance = null;
        public string ConnectionString { get; }
        private ConfigController()
        {
            var ymlString = Loader.LoadConfigFile("./server.yaml");
            if (ymlString == null)
            {
                return;
            }
            
            Debug.WriteLine(ymlString);
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