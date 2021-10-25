using CityOfMindDatabase.Contexts;

namespace CityOfMindPluginBase
{
  public interface IPlugin
  {
    void Start(IPluginApi api);
  }
}