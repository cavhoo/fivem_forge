using System;
using CitizenFX.Core;

namespace CityOfMindPluginBase
{
  public interface IPluginApi
  {
    void RegisterEvent(string eventName, Action<Player, string> callback);
    void TriggerEvent(string eventName);
    void TriggerEvent(string eventName, params object[] data);
    void TriggerClientEvent(string eventName);
    void TriggerClientEvent(string eventName, params object[] data);
  }
}