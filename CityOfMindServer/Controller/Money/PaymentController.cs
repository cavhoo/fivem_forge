using System;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using FiveMForge.Controller.Base;
using FiveMForge.Database;
using CityOfMindDatabase;
using FiveMForge.Models;
using static CitizenFX.Core.Native.API;

namespace FiveMForge.Controller.Money
{
    /// <summary>
    /// Class <c>PaymentController</c>
    /// Controls the whole payment/transaction system.
    /// Processing of job payouts, banking transfers and paying at
    /// shops with your card will be handled here.
    /// </summary>
    public class PaymentController : BaseClass
    {
        public PaymentController()
        {
            //ProcessJobPayments();
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
                TriggerEvent(ServerEvents.CreatePayments);
            }

            // Process payments in pending table
            await ProcessPendingTransactions();
        }

        private async Task ProcessPendingTransactions()
        {
            var nextTransaction = Context.PendingBankTransactions.FirstOrDefault();

            // No transactions found.
            if (nextTransaction == null) return;

            var sourceAccount =
                Context.BankAccount.FirstOrDefault(b => b.AccountNumber == nextTransaction.FromAccountNumber);
            if (sourceAccount == null) return;

            var targetAccount = Context.BankAccount.FirstOrDefault(t => t.AccountNumber == nextTransaction.ToAccountNumber);
            if (targetAccount == null) return;

            if (sourceAccount.Saldo >= nextTransaction.Amount)
            {
                // Deduct transfer amount from source account.
                sourceAccount.Saldo -= nextTransaction.Amount;
                targetAccount.Saldo += nextTransaction.Amount;
                var bankTransaction = new BankTransaction();
                bankTransaction.FromAccountNumber = nextTransaction.FromAccountNumber;
                bankTransaction.ToAccountNumber = nextTransaction.ToAccountNumber;
                bankTransaction.Message = nextTransaction.Message;
                bankTransaction.Amount = nextTransaction.Amount;
                Context.PendingBankTransactions.Remove(nextTransaction);
                Context.BankTransactions.Add(bankTransaction);
            }

            await Context.SaveChangesAsync();
        }
    }
}