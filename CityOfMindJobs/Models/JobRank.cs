using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityOfMindJobs
{
  public class JobRank
  {
    [Key]
    public string Uuid { get; set; }
    public string Title { get; set; }
    public int Salary { get; set; }
    public string? NextRankUuid { get; set; }
    public string? PreviousRankUuid { get; set; }
    public JobRank PreviousRank { get; set; }

    public JobRank(string title, int salary, JobRank nextRank, JobRank previousRank)
    {
      Uuid = Guid.NewGuid().ToString();
      Title = title;
      Salary = salary;
      NextRankUuid = nextRank?.Uuid;
      PreviousRankUuid = previousRank?.Uuid;
    }
  }
}