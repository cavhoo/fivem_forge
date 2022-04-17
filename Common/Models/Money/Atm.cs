using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models.Money
{
    [Table("Atms")]
    public class Atm
    {
        [Key]
        public int Id { get; set; }
        public string Location { get; set; }
    }
}