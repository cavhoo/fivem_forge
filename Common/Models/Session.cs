using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    public class Session
    {
        public string SessionId { get; set; }
        public string SessionToken { get; set; }
        public string ExpirationDate { get; set; }
        
        [InverseProperty("Uuid")]
        public string AccountUuid { get; set; }
        public Player.Player SessionPlayer { get; set; }
    }
}