using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models.Factions
{
  public class FactionRank
  {
    [Key]
    public string Uuid { get; set; } // Identifier for the faction rank
    public string Name { get; set; } // Name of the faction rank
    public int Salary { get; set; } // Salary of the faction, this is paid in addition to any job the player has
    
    public Faction Faction { get; set; }
    [ForeignKey("FactionId")] 
    public string FactionId { get; set; } // The faction this rank belongs to.
  }
}