using System.ComponentModel.DataAnnotations.Schema;

namespace FiveMForge.Models
{
    public class BankTransaction
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Message { get; set; }
        public TransactionStatus Status { get; set; }
        
        [InverseProperty("AccountNumber")]
        public string FromAccountNumber { get; set; }
        public BankAccount FromAccount { get; set; }
        
        [InverseProperty("AccountNumber")] 
        public string ToAccountNumber { get; set; }
        public BankAccount ToAccount { get; set; }
    }
}