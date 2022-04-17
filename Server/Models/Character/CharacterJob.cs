using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Character
{
  public class CharacterJob
  {
    [Key]
    public int Id { get; set; }
    [ForeignKey("Character")] public string CharacterUuid;
    [ForeignKey("JobRank")] public string JobUuid;
  }
}