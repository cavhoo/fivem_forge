﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CitizenFX.Core;
using CityOfMindPluginBase;
using FiveMForge.Config;

namespace FiveMForge.Controller.Plugins
{
  public class PluginLoader
  {
    public List<IPlugin> LoadPlugins()
    {
      var loadedPlugins = new List<IPlugin>();
      if (!Directory.Exists("./plugins"))
      {
        return loadedPlugins;
      }

      var searchPattern = "*.plugin.dll";
      DirectoryInfo directory = new DirectoryInfo("./plugins");
      Debug.WriteLine($"{directory.FullName}");
      Debug.WriteLine(
        $"Trying to load {directory.GetFiles(searchPattern).Count()} plugins with pattern {searchPattern}");

      FileInfo[] plugins = directory.GetFiles(searchPattern);
      foreach (var plugin in plugins)
      {
        Debug.WriteLine($"Loading plugin: {plugin.FullName}");
        Assembly.LoadFile(plugin.FullName);
      }

      var pluginType = typeof(IPlugin);
      Debug.WriteLine("Filtering for plugins...");
      var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
        .Where(p => pluginType.IsAssignableFrom(p) && p.IsClass).ToArray();
      Debug.WriteLine("Loading plugins...");
      foreach (var type in types)
      {
        Debug.WriteLine($"Instantiating Pluging {type}");
        Debug.WriteLine(Config.ConfigController.GetInstance().ConnectionString);
        //loadedPlugins.Add((IPlugin)Activator.CreateInstance(type, new {ConfigController.GetInstance().ConnectionString}));
      }
      return loadedPlugins;
    }
  }
}