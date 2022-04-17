using Common.Models.Enums;

namespace Common.Models.Player
{
    public class Tiers
    {
        public int Id { get; set; }
        public int CharacterSlots { get; set; }
        public PlayerTier Tier { get; set; }
    }
}