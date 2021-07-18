using FiveMForge.Models.Enums;

namespace FiveMForge.Models
{
    public class Tiers
    {
        public int Id { get; set; }
        public int CharacterSlots { get; set; }
        public PlayerTier Tier { get; set; }
    }
}