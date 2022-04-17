using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("Atms")]
    public class Atm
    {
        [Key]
        public int Id { get; set; }
        public string Location { get; set; }
    }
}