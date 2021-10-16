
using System;
using System.Data.Entity;

namespace CityOfMindJobs.Context
{
  public class JobContext : CityOfMindDatabase.Contexts.Context
  {
    public JobContext(): base("JobContext")
    {
      System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<JobContext, Configuration>());
    }
   public JobContext(string connectionString) : base(connectionString)
    {
      System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<JobContext, Configuration>());
    }

    public DbSet<Job> Jobs { get; set; }
    public DbSet<JobRank> JobRanks { get; set; }
    public DbSet<CharacterJob> CharacterJobs { get; set; }
  }
}