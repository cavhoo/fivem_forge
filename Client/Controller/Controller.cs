using System;
using CitizenFX.Core;

namespace Client.Controller
{
  public class Controller
  {
        protected EventHandlerDictionary EventHandlers;
        protected Action<string, object[]> TriggerEventFunc;
        protected Action<Player, string, object[]> TriggerClientEventFunc;
  }
}