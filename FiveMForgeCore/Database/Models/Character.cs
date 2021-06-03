using System.ComponentModel.DataAnnotations.Schema;

namespace FiveMForge.Database.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string LastPos { get; set; }
        public string Uuid { get; set; }
        public bool InUse { get; set; }
        
        public string CharacterUuid { get; set; }
        
        [InverseProperty("Uuid")]
        public string JobUuid { get; set; }
        public Job Job { get; set; }
    }
}