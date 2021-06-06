using System.ComponentModel.DataAnnotations.Schema;

namespace FiveMForge.Models
{
    public class BankAccount
    {
       public int Id { get; set; }
       public string AccountNumber { get; set; }
       public int Saldo { get; set; }
       [InverseProperty("Uuid")]
       public string Holder { get; set; }
       public Character Character { get; set; }
    }
}