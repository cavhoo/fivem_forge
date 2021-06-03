using System;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using FiveMForge.Controller.Base;
using FiveMForge.Database;
using FiveMForge.Database.Contexts;
using FiveMForge.Database.Models;
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

            if (currentUtcTime.Minute % 10 == 0)
            {
                // Payout salary from jobs
                CreateJobPayments();
            }

            // Process payments in pending table
            ProcessPendingTransactions();
        }

        private async void CreateJobPayments()
        {
            using var ctx = new CoreContext();
            var employedCharacters = ctx.Characters.Where(c => c.Job != null).ToList();
            
            
            
            foreach (var character in employedCharacters)
            {
                var player = ctx.Players.FirstOrDefault(p => p.Uuid == character.Uuid);
                if (player == null) return;
                
                var bankAccount = ctx.BankAccount.FirstOrDefault(account => account.Holder == character.CharacterUuid);
                if (bankAccount == null) continue;
                var job = ctx.Jobs.FirstOrDefault(j => j.Uuid == character.JobUuid);
                if (job != null)
                {
                    var salary = job.Salary;
                    var pendingTransaction = new PendingBankTransaction();
                    pendingTransaction.FromAccountNumber = "";
                    pendingTransaction.ToAccountNumber = bankAccount.AccountNumber;
                    pendingTransaction.Amount = salary;
                    pendingTransaction.Message = $"Job Payment for {job.Title}";
                    ctx.PendingBankTransactions.Add(pendingTransaction);
                }
            }
            await ctx.SaveChangesAsync();
        }

        private async void ProcessPendingTransactions()
        {
            using var ctx = new CoreContext();
            var nextTransaction = ctx.PendingBankTransactions.FirstOrDefault();

            // No transactions found.
            if (nextTransaction == null) return;

            var sourceAccount = ctx.BankAccount.FirstOrDefault(b => b.AccountNumber == nextTransaction.FromAccountNumber);
            if (sourceAccount == null) return;
            
            var targetAccount = ctx.BankAccount.FirstOrDefault(t => t.AccountNumber == nextTransaction.ToAccountNumber);
            if (targetAccount == null) return;
            
            
            // Deduct transfer amount from source account.
            sourceAccount.Saldo -= nextTransaction.Amount;
            targetAccount.Saldo += nextTransaction.Amount;

            var bankTransaction = new BankTransaction();
            bankTransaction.FromAccountNumber = nextTransaction.FromAccountNumber;
            bankTransaction.ToAccountNumber = nextTransaction.ToAccountNumber;
            bankTransaction.Message = nextTransaction.Message;
            bankTransaction.Amount = nextTransaction.Amount;
            ctx.PendingBankTransactions.Remove(nextTransaction);
            ctx.BankTransactions.Add(bankTransaction);
        }

        private async void DeductFromSourceBankAccount(decimal amount, string accountNumber)
        {
            using (var db = new DbConnector())
            {
                await db.Connection.OpenAsync();
                var deductFromAccountCommand = new MySqlCommand();
                deductFromAccountCommand.Connection = db.Connection;
                deductFromAccountCommand.CommandText = $"update bankAccount set saldo = saldo - {amount} where accountNumber = {accountNumber}";

                await deductFromAccountCommand.ExecuteNonQueryAsync();
            }

        }
    }
}
