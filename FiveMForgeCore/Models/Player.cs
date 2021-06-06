using System.ComponentModel.DataAnnotations;

namespace FiveMForge.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string LastLogin { get; set; }
        public string AccountId { get; set; }
        [Key]
        public string Uuid { get; set; }
        public Tiers Tier { get; set; }
    }
}