using Server.Models.Enums;

namespace Server.Models
{
    public class TiersTemplates
    {
        public static Tiers Common = new Tiers {CharacterSlots = 1, Tier = PlayerTier.COMMON};
        public static Tiers Supporter = new Tiers {CharacterSlots = 2, Tier = PlayerTier.SUPPORTER};
        public static Tiers Vip = new Tiers {CharacterSlots = 4, Tier = PlayerTier.VIP};
        public static Tiers Admin = new Tiers {CharacterSlots = 6, Tier = PlayerTier.ADMIN};
        public static Tiers Owner = new Tiers {CharacterSlots = 10, Tier = PlayerTier.OWNER};
    }
}