using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Models.Jobs
{
  public class Job
  {
    public int Id { get; set; }
    [Key]
    public string Uuid { get; set; }
    public string Title { get; set; }

    public Job()
    {
      
    }
    public Job(string title)
    {
      Uuid = Guid.NewGuid().ToString();
      Title = title;
    }
  }
}