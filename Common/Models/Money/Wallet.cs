using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models.Money
{
    public class Wallet
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int Saldo { get; set; }
        
        [InverseProperty("Uuid")]
        public string Holder { get; set; }
        public Common.Models.Character.Character Character { get; set; }
    }
}