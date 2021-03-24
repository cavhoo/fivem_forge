using System;
using System.Collections.Generic;
using CitizenFX.Core;
using FiveMForge.database;
using MySqlConnector;
using static CitizenFX.Core.Native.API;

namespace FiveMForge
{
    public class Core : BaseScript
    {
      public Core()
      {
        this.InitializeDatabase();
      }

      private async void InitializeDatabase()
      {
          Debug.WriteLine("FiveM Forge Starting...verifying database...");
          Debug.WriteLine("Checking if all tables exist, if not then we create them :)");
          using var db = new DbConnector();
          await db.Connection.OpenAsync();
          // Create Player Table if not exists
          var createPlayerTableCmd = new MySqlCommand();
          createPlayerTableCmd.Connection = db.Connection;
          createPlayerTableCmd.CommandText = "CREATE TABLE IF NOT EXISTS players (last_login varchar(255), account_id varchar(255), uuid varchar(255), primary key (uuid))";
          await createPlayerTableCmd.ExecuteNonQueryAsync();

          var createMoneyTable = new MySqlCommand();
          createMoneyTable.Connection = db.Connection;
          createMoneyTable.CommandText = "create table if not exists money (id int auto_increment, currency varchar(50), primary key(id))";
          await createMoneyTable.ExecuteNonQueryAsync();

          var createCharacterTable = new MySqlCommand();
          createCharacterTable.Connection = db.Connection;
          createCharacterTable.CommandText = "create table if not exists characters (id int auto_increment, last_pos varchar(255), in_use bool, uuid varchar(255), primary key (id, uuid), foreign key (uuid) references players(uuid))";
          await createCharacterTable.ExecuteNonQueryAsync();
      }
    }

}
