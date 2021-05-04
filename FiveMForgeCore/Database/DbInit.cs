using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveMForge.Models;
using MySqlConnector;

namespace FiveMForge.Database
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
            createPlayerTableCmd.CommandText =
                "CREATE TABLE IF NOT EXISTS players (last_login varchar(254), account_id varchar(255), uuid varchar(255), sessionId varchar(255), primary key (uuid))";
            await createPlayerTableCmd.ExecuteNonQueryAsync();

            var createMoneyTable = new MySqlCommand();
            createMoneyTable.Connection = db.Connection;
            createMoneyTable.CommandText =
                "create table if not exists money (id int auto_increment, currency varchar(49), primary key(id))";
            await createMoneyTable.ExecuteNonQueryAsync();

            var createCharacterTable = new MySqlCommand();
            createCharacterTable.Connection = db.Connection;
            createCharacterTable.CommandText =
                "create table if not exists characters (id int auto_increment, last_pos varchar(254), in_use bool, uuid varchar(255), primary key (id, uuid), foreign key (uuid) references players(uuid))";
            await createCharacterTable.ExecuteNonQueryAsync();

            var createBankingTable = new MySqlCommand();
            createBankingTable.Connection = db.Connection;
            createBankingTable.CommandText =
                "create table if not exists banks (id int auto_increment, name varchar(255), isActive bool, isAdminOnly bool, location varchar(255), primary key (id))";
            await createBankingTable.ExecuteNonQueryAsync();

            var createAtmTable = new MySqlCommand();
            createAtmTable.Connection = db.Connection;
            createAtmTable.CommandText =
                "create table if not exists atms (id int auto_increment, location varchar(255), primary key (id))";
            await createAtmTable.ExecuteNonQueryAsync();
            
            // var createBankAccountTable = new MySqlCommand();
            // createBankAccountTable.Connection = db.Connection;
            // createBankAccountTable.CommandText =
            //     "create table if not exists  bankAccount (id int auto_increment, holder varchar(255), accountNumber varchar(255), saldo number, primary key (id, accountNumber))";
            // await createBankAccountTable.ExecuteNonQueryAsync();
            //
            // var createTransactionTable = new MySqlCommand();
            // createTransactionTable.Connection = db.Connection;
            // createTransactionTable.CommandText =
            //     "create table if not exists bankTransactions (id int auto_increment, from_account_number varchar(255), to_account_number varchar(255), amount number, message varchar(255), primary key (id, from_account_number, to_account_number), foreign key (from_account_number) references bankAccount(accountNumber), foreign key (to_account_number) references bankAccount(accountNumber))";
            // await createTransactionTable.ExecuteNonQueryAsync();
                
            //PopulateTables();

            var checkAtmTableCommand = new MySqlCommand();
            checkAtmTableCommand.CommandText = "select * from atms";
            checkAtmTableCommand.Connection = db.Connection;
            var atmRows = await checkAtmTableCommand.ExecuteReaderAsync();
            await atmRows.ReadAsync();
            if (!atmRows.HasRows)
            {
                var commandString = new StringBuilder($"insert into atms (location) values ");
                var atmLocations = AtmLocations.Locations
                    .Select(location => $"('{location.X}:{location.Y}:{location.Z}')").ToList();

                commandString.Append(string.Join(",", atmLocations));
                commandString.Append(";");
                var atmRowCommand = new MySqlCommand(commandString.ToString(), db.Connection);
                await atmRowCommand.ExecuteNonQueryAsync();
            }

            atmRows.Close();

            var checkBankTableCommand = new MySqlCommand();
            checkBankTableCommand.CommandText = "select * from banks";
            checkBankTableCommand.Connection = db.Connection;
            var bankRows = await checkBankTableCommand.ExecuteReaderAsync();
            await bankRows.ReadAsync();
            if (!bankRows.HasRows)
            {
                bankRows.Close();
                var commandString =
                    new StringBuilder($"insert into banks (name, isActive, isAdminOnly, location) values ");
                var bankLocations = BankLocations.Locations
                    .Select(banklocation => $"('{banklocation.Name}', {banklocation.IsActive}, {banklocation.IsAdminOnly}, '{banklocation.X}:{banklocation.Y}:{banklocation.Z}')").ToList();

                commandString.Append(string.Join(",", bankLocations));
                commandString.Append(";");
                var bankRowCommand = new MySqlCommand(commandString.ToString(), db.Connection);
                await bankRowCommand.ExecuteNonQueryAsync();
            }
        }
    }
}