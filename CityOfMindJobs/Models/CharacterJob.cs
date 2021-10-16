using System.ComponentModel.DataAnnotations;

namespace CityOfMindJobs
{
  public class CharacterJob
  {
    public int Id { get; set; }
    public string CharacterUuid;
    public string JobUuid;
  }
}