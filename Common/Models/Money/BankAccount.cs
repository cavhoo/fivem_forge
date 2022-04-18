using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models.Money
{
    public class BankAccount
    {
       public int Id { get; set; }
       [Key]
       public string AccountNumber { get; set; }
       public int Saldo { get; set; }
       public Common.Models.Character.Character Character { get; set; }
       [ForeignKey("Character")]
       public string Holder { get; set; }
    }
}