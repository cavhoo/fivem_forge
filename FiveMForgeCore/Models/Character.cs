using System.ComponentModel.DataAnnotations.Schema;

namespace FiveMForge.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string LastPos { get; set; }
        public string Uuid { get; set; }
        public bool InUse { get; set; }
        
        [InverseProperty("Uuid")]
        public string AccountUuid { get; set; }
        public Player Owner { get; set; }
        
        [InverseProperty("Uuid")]
        public string JobUuid { get; set; }
        public Job Job { get; set; }
    }
}