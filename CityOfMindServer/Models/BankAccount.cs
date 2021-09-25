using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiveMForge.Models
{
    public class BankAccount
    {
       public int Id { get; set; }
       [Key]
       public string AccountNumber { get; set; }
       public int Saldo { get; set; }
       public Character Character { get; set; }
       [ForeignKey("Character")]
       public string Holder { get; set; }
    }
}