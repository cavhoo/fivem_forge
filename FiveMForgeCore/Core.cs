using System;
using System.Collections.Generic;
using CitizenFX.Core;
using FiveMForge.database;
using MySqlConnector;
using static CitizenFX.Core.Native.API;

namespace FiveMForgeCore
{
    public class Core : BaseScript
    {
      public Core()
      {
        this.InitializeDatabase();
      }

      private void InitializeDatabase()
      {
          Debug.WriteLine("FiveM Forge Starting...verifying database...");
          Debug.WriteLine("Checking if all tables exist, if not then we create them :)");
          DbInit.CreateTables();
      }
    }

}
