
using System;
using System.Data.Entity;
using CitizenFX.Core;

namespace CityOfMindJobs.Context
{
  public class JobContext : CityOfMindDatabase.Contexts.Context
  {
   public JobContext(string connectionString) : base(connectionString)
    {
      Debug.WriteLine("Plugin Contructor");
      System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<JobContext, Configuration>());
    }

    public DbSet<Job> Jobs { get; set; }
    public DbSet<JobRank> JobRanks { get; set; }
    public DbSet<CharacterJob> CharacterJobs { get; set; }
  }
}