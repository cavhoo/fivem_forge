using System;
using System.Diagnostics.Eventing.Reader;
using CitizenFX.Core;
using CityOfMindPluginBase;

namespace FiveMForge.Controller.Plugins
{
  public class PluginApi : IPluginApi
  {
    protected EventHandlerDictionary EventHandler;
    protected Action<string,object[]> TriggerEventFunc;
    protected Action<string,object[]> TriggerClientEventFunc;
    public PluginApi(EventHandlerDictionary handlers, Action<string, object[]> eventTriggerFunc,
      Action<Player, string, object[]> clientEventTriggerFunc)
    {
      TriggerEventFunc = eventTriggerFunc;
      TriggerClientEventFunc = eventTriggerFunc;
      EventHandler = handlers;
    }
    
    /// <summary>
    /// Register an Event that is triggered from client or server side.
    /// The callback is always called with the player and a string containing
    /// JSON data if there is any. 
    /// </summary>
    /// <param name="eventName">Name of the event to listen to.</param>
    /// <param name="callback">Function that should be called when said event is done.</param>
    public void RegisterEvent(string eventName, Action<Player, string> callback)
    {
      EventHandler[eventName] += callback;
    }

    /// <summary>
    /// Trigger a plain event with no payload.
    /// </summary>
    /// <param name="eventName">Name of the event that should be triggered.</param>
    public void TriggerEvent(string eventName)
    {
      TriggerEventFunc(eventName, null);
    }

    /// <summary>
    /// Trigger an event with a JSON or String payload.
    /// </summary>
    /// <param name="eventName">Name of the event that should be triggered.</param>
    /// <param name="args">Payload that should be sent with the event, either JSON Data or string.</param>
    public void TriggerEvent(string eventName, params object[] args)
    {
      TriggerEventFunc(eventName, args);
    }

    /// <summary>
    /// Trigger a plain client event.
    /// </summary>
    /// <param name="eventName">Name of the event that should be triggered.</param>
    public void TriggerClientEvent(string eventName)
    {
      TriggerClientEventFunc(eventName, null);
    }

    /// <summary>
    /// Trigger a client event with a payload that's either JSON or a plain string.
    /// </summary>
    /// <param name="eventName">Name of the event that should be triggered.</param>
    /// <param name="args">Data as event payload.</param>
    public void TriggerClientEvent(string eventName, params object[] args)
    {
      TriggerClientEventFunc(eventName, args);
    }
  }
}