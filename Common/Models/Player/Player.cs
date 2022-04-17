using System.ComponentModel.DataAnnotations;
using Common.Models.Enums;

namespace Common.Models.Player
{
    public class Player
    {
        public int Id { get; set; }
        public string LastLogin { get; set; }
        public string AccountId { get; set; }
        [Key]
        public string AccountUuid { get; set; }
        public PlayerTier Tier { get; set; }
    }
}