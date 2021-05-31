using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using FiveMForge.Controller.Base;
using FiveMForge.Database;
using MySqlConnector;
using static CitizenFX.Core.Native.API;

namespace FiveMForge.Controller.Money
{
    public class PaymentController : BaseClass
    {
        public PaymentController()
        {
            ProcessJobPayments();
        }

        private void ProcessJobPayments()
        {
            Tick += OnTick;
        }

        private async Task OnTick()
        {
            DateTime currentUtcTime = DateTime.UtcNow;
            if (currentUtcTime.Hour == 12 && currentUtcTime.Minute == 0 && currentUtcTime.Second == 0)
            {
                // Payout salary from jobs
                CreateJobPayments();
            }

            // Process payments in pending table
            ProcessPendingTransactions();
        }

        private async void CreateJobPayments()
        {
            using (var connector = new DbConnector())
            {
                await connector.Connection.OpenAsync();

                var activeJobsCommand = new MySqlCommand();
                activeJobsCommand.Connection = connector.Connection;
                activeJobsCommand.CommandText = "select characterUuid, jobId from characters where jobId is not null";

                var activeJobsList = await activeJobsCommand.ExecuteReaderAsync();
                await activeJobsList.ReadAsync();
                if (!activeJobsList.HasRows) return;

                while (activeJobsList.Read())
                {
                    var charUuid = activeJobsList.GetString("characterUuid");
                    // Load bank account for character
                    var bankAccountCommand = new MySqlCommand();
                    bankAccountCommand.Connection = connector.Connection;
                    bankAccountCommand.CommandText = $"select * from bankAccount where holder = `{charUuid}`";
                    var bankAccountReader = await bankAccountCommand.ExecuteReaderAsync();

                    await bankAccountReader.ReadAsync();
                    if (!bankAccountReader.HasRows) return;


                    var jobDataCommand = new MySqlCommand();
                    jobDataCommand.Connection = connector.Connection;
                    jobDataCommand.CommandText = "select title, salary from jobs";
                    var jobDataReader = await jobDataCommand.ExecuteReaderAsync();
                    await jobDataReader.ReadAsync();

                    if (!jobDataReader.HasRows) return;

                    var salary = jobDataReader.GetString("salary");
                    
                    var accountNumber = bankAccountReader.GetString("accountNumber");
                    
                    var createPendingTransactionCommand = new MySqlCommand();
                    createPendingTransactionCommand.Connection = connector.Connection;
                    createPendingTransactionCommand.CommandText =
                        $"insert into pendingBankTransactions values ('Job', {accountNumber}, 'Salary', {salary})";
                    await createPendingTransactionCommand.ExecuteNonQueryAsync();

                }

            }
        }

        private async void ProcessPendingTransactions()
        {
            using (var connector = new DbConnector())
            {
                await connector.Connection.OpenAsync();

                var pendingTransactionsCommand = new MySqlCommand();
                pendingTransactionsCommand.Connection = connector.Connection;
                pendingTransactionsCommand.CommandText = "select * from pendingBankTransactions";

                var result = await pendingTransactionsCommand.ExecuteReaderAsync();
                await result.ReadAsync();

                // Early abort if no transactions are pending
                if (!result.HasRows) return;

                while (result.Read())
                {
                    // book amount to target bank account
                    // Add transaction to transactions table
                    // delete row from pending transactions
                }
            }
        }
    }
}