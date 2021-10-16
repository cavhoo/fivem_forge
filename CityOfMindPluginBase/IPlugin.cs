using CityOfMindDatabase.Contexts;

namespace CityOfMindPluginBase
{
  public interface IPlugin
  {
    void Start(string connectionString, IPluginApi api);
  }
}