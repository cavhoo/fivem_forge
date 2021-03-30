using MySqlConnector;

namespace FiveMForge.database
{
    public class DbInit
    {
        public static async void CreateTables()
        {
          using var db = new DbConnector();
          await db.Connection.OpenAsync();
          // Create Player Table if not exists
          var createPlayerTableCmd = new MySqlCommand();
          createPlayerTableCmd.Connection = db.Connection;
          createPlayerTableCmd.CommandText = "CREATE TABLE IF NOT EXISTS players (last_login varchar(254), account_id varchar(255), uuid varchar(255), primary key (uuid))";
          await createPlayerTableCmd.ExecuteNonQueryAsync();

          var createMoneyTable = new MySqlCommand();
          createMoneyTable.Connection = db.Connection;
          createMoneyTable.CommandText = "create table if not exists money (id int auto_increment, currency varchar(49), primary key(id))";
          await createMoneyTable.ExecuteNonQueryAsync();

          var createCharacterTable = new MySqlCommand();
          createCharacterTable.Connection = db.Connection;
          createCharacterTable.CommandText = "create table if not exists characters (id int auto_increment, last_pos varchar(254), in_use bool, uuid varchar(255), primary key (id, uuid), foreign key (uuid) references players(uuid))";
          await createCharacterTable.ExecuteNonQueryAsync();
        }
    }
}