using System.ComponentModel.DataAnnotations;

namespace Common.Models.Factions
{
  public class Faction
  {
    [Key]
    public string Uuid { get; set; }
    public string Name { get; set; }
    public string[] Jobs { get; set; }
    
  }
}