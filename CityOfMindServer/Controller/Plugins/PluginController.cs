using System;
using System.Collections.Generic;
using System.Reflection;
using CitizenFX.Core;
using CityOfMindPluginBase;
using FiveMForge.Config;
using FiveMForge.Controller.Base;
using FiveMForge.Models;

namespace FiveMForge.Controller.Plugins
{
  public class PluginController : BaseClass
  {
    private PluginLoader loader;
    private readonly List<IPlugin> loadedPlugins;

    public PluginController()
    {
      loader = new PluginLoader();
      loadedPlugins = loader.LoadPlugins();

      foreach (var plugin in loadedPlugins)
      {
        plugin.Start(new PluginApi(EventHandlers, TriggerEvent, TriggerClientEvent));
      }
    }
  }
}