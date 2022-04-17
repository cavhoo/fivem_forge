namespace Server.Models.Jobs
{

  public class JobEntry
  {
     public string Title { get; set; }
     public JobRank[] Grades { get; set; }
  }
  
  public class JobsConfig
  {
    public JobEntry[] Jobs {
      get;
      set;
    }
  }
}