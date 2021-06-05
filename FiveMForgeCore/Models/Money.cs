using System.Data.Common;

namespace FiveMForge.Database.Models
{
    public class Money
    {
        public int Id { get; set; }
        public string CurrencyCode { get; set; }
        public int Factor { get; set; }
    }
}