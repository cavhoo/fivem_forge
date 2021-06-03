namespace FiveMForge.Database.Models
{
    public class BankAccount
    {
       public int Id { get; set; }
       public string Holder { get; set; }
       public string AccountNumber { get; set; }
       public int Saldo { get; set; }
    }
}