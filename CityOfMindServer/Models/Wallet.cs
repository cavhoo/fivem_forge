using System.ComponentModel.DataAnnotations.Schema;

namespace FiveMForge.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int Saldo { get; set; }
        
        [InverseProperty("Uuid")]
        public string Holder { get; set; }
        public Character Character { get; set; }
    }
}