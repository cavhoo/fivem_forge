using Server.Models.Enums;

namespace Server.Models
{
    public class Tiers
    {
        public int Id { get; set; }
        public int CharacterSlots { get; set; }
        public PlayerTier Tier { get; set; }
    }
}