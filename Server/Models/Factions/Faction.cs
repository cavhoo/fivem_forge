using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
  public class Faction
  {
    // Unique Identifier for Factions
    [Key]
    public string Uuid { get; set; }
    // Name displayed on the UI for the faction the player is a member of
    public string Name { get; set; }
    // List of Jobs the faction has control over
    public string[] Jobs { get; set; }
    
  }
}