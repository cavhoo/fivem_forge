using System;
using CitizenFX.Core;
using Server.Controller.Config;
using Server.Context;

namespace Server.Controller.Base
{
  /// <summary>
  /// BaseClass
  /// Foundation for all C# Server-Side code.
  /// </summary>
  public class BaseClass
  {
    protected CoreContext Context;
    protected EventHandlerDictionary EventHandlers;
    protected Action<string, object[]> TriggerEventFunc;
    protected Action<Player, string, object[]> TriggerClientEventFunc;
    protected BaseClass() => Context = new CoreContext();

    public BaseClass(EventHandlerDictionary handlers, Action<string, object[]> eventTriggerFunc,
      Action<Player, string, object[]> clientEventTriggerFunc)
    {
      EventHandlers = handlers;
      TriggerEventFunc = eventTriggerFunc;
      TriggerClientEventFunc = clientEventTriggerFunc;
      Context = new CoreContext();
    }

    /// <summary>
    /// Trigger a plain event with no payload.
    /// </summary>
    /// <param name="eventName">Name of the event that should be triggered.</param>
    protected void TriggerEvent(string eventName)
    {
      TriggerEventFunc(eventName, null);
    }

    /// <summary>
    /// Trigger an event with a JSON or String payload.
    /// </summary>
    /// <param name="eventName">Name of the event that should be triggered.</param>
    /// <param name="args">Payload that should be sent with the event, either JSON Data or string.</param>
    protected void TriggerEvent(string eventName, params object[] args)
    {
      TriggerEventFunc(eventName, args);
    }

    /// <summary>
    /// Trigger a plain client event.
    /// </summary>
    /// <param name="player"> The player the event should be triggered on.</param>
    /// <param name="eventName">Name of the event that should be triggered.</param>
    protected void TriggerClientEvent(Player player, string eventName)
    {
      TriggerClientEventFunc(player, eventName, null);
    }

    /// <summary>
    /// Trigger a client event with a payload that's either JSON or a plain string.
    /// </summary>
    /// <param name="player">The player the event should be triggered on.</param>
    /// <param name="eventName">Name of the event that should be triggered.</param>
    /// <param name="args">Data as event payload.</param>
    protected void TriggerClientEvent(Player player, string eventName, params object[] args)
    {
      TriggerClientEventFunc(player, eventName, args);
    }
  }
}