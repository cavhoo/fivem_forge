using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CityOfMindJobs
{
  public class Job
  {
    [Key]
    public int Uuid { get; set; }
    public string Title { get; set; }
    public List<JobRank> AvailableRanks { get; set; }
  }
}