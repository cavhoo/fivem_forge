using System;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using CityOfMindDatabase;
using Server.Controller.Base;
using Server.Models;
using static CitizenFX.Core.Native.API;
using Player = CitizenFX.Core.Player;

namespace Server.Controller.Money
{
    /// <summary>
    /// Class <c>PaymentController</c>
    /// Controls the whole payment/transaction system.
    /// Processing of job payouts, banking transfers and paying at
    /// shops with your card will be handled here.
    /// </summary>
    public class PaymentController : BaseClass
    {
        private bool CreatingPayments = false;
        public PaymentController(EventHandlerDictionary handlers, Action<string, object[]> eventTriggerFunc,
                                                                    Action<Player, string, object[]> clientEventTriggerFunc, Action<string, object[]> clientEventTriggerAllFunc): base(handlers, eventTriggerFunc, clientEventTriggerFunc, clientEventTriggerAllFunc)
        {
            Debug.WriteLine("Started PaymentController");
        }
        
        public async Task OnTick()
        {
            DateTime currentUtcTime = DateTime.UtcNow;
            if (currentUtcTime.Minute % 10 == 0 && CreatingPayments == false)
            {
                // Payout salary from jobs
                CreateJobPayments();
                CreatingPayments = true;
            }

            // Process payments in pending table
            ProcessPendingTransactions();
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
                Debug.WriteLine($"Processing Transaction from Account: {sourceAccount.AccountNumber} to Account: {targetAccount.AccountNumber}");
                // Deduct transfer amount from source account.
                sourceAccount.Saldo -= nextTransaction.Amount;
                targetAccount.Saldo += nextTransaction.Amount;
                var bankTransaction = new BankTransaction();
                bankTransaction.FromAccountNumber = nextTransaction.FromAccountNumber;
                bankTransaction.ToAccountNumber = nextTransaction.ToAccountNumber;
                bankTransaction.Message = nextTransaction.Message;
                bankTransaction.Amount = nextTransaction.Amount;

                // Add a timestamp when the transaction happened
                var utcNow = DateTime.UtcNow;
                bankTransaction.TimeStamp = $"{utcNow.ToShortDateString()} {utcNow.ToLongTimeString()}";
               
                Context.PendingBankTransactions.Remove(nextTransaction);
                Context.BankTransactions.Add(bankTransaction);
            }

            await Context.SaveChangesAsync();
        }

        private void CreateJobPayments()
        {
            // Get all online characters
            var characters = Context.Characters.Where(c => c.InUse == true).ToList();

            foreach (var character in characters)
            {
                var activeJob = Context.JobRanks.FirstOrDefault(jr => jr.Uuid == character.JobUuid);
                if (activeJob == null)
                {
                    Debug.WriteLine("No active job. Skipping");
                    continue;
                }
                var company = Context.Jobs.FirstOrDefault(j => j.Uuid == activeJob.JobId);

                if (company == null)
                {
                    Debug.WriteLine("No active company. Skipping.");
                    continue;
                }

                // Company bankaccount
                var companyBankAccount = Context.BankAccount.FirstOrDefault(b => b.Holder == company.Uuid);

                
                // Refill the Arbeitslos account to serve all payments.
                if (company.Title == "Arbeitslos")
                {
                    companyBankAccount.Saldo = 500000;
                }
                
                // Character bankaccount
                var bankAccount = Context.BankAccount.FirstOrDefault(b => b.Holder == character.CharacterUuid);
                if (bankAccount == null)
                {
                    Debug.WriteLine("No bankaccount found.");
                    continue;
                }
                
                // Create pending transaction
                var jobPaymentTransaction = new PendingBankTransaction()
                {
                    Amount = activeJob.Salary,
                    Message = "Gehalt",
                    ToAccountNumber = bankAccount.AccountNumber,
                    FromAccountNumber = companyBankAccount.AccountNumber,
                    Status = TransactionStatus.PENDING
                };

                Context.PendingBankTransactions.Add(jobPaymentTransaction);
            }

            Context.SaveChanges();
            CreatingPayments = false;
        }

        private void CreateFactionPayments()
        {

        }
    }
}