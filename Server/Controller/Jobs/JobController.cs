using System;
using System.Linq;
using CitizenFX.Core;
using Newtonsoft.Json;
using Server.Controller.Base;
using Server.Controller.Config;
using Server.Models;
using Server.Models.Jobs;
using Server.Utils;
using Player = CitizenFX.Core.Player;

namespace Server.Controller.Jobs
{
  public class JobController : BaseClass
  {
    public JobController(EventHandlerDictionary handlers, Action<string, object[]> eventTriggerFunc,
      Action<Player, string, object[]> clientEventTriggerFunc) : base(handlers, eventTriggerFunc,
      clientEventTriggerFunc)
    {
      LoadJobConfigFile();
    }

    protected void LoadJobConfigFile()
    {
      var configPath = ConfigController.GetInstance().Config.JobConfigPath;
      Debug.WriteLine("Loading job config file...");
      var jobConfigFile = "";
      try
      {
        jobConfigFile = Loader.LoadConfigFile(configPath);
        Debug.WriteLine("Config found. Parsing Job Data...");
      }
      catch (Exception e)
      {
        Console.WriteLine("No Config Found...{0}", e);
        return;
      }

      var parsedConfig = JsonConvert.DeserializeObject<JobsConfig>(jobConfigFile);

      if (parsedConfig == null)
      {
        Debug.WriteLine("Could not parse job config file. ");
        return;
      }

      if (parsedConfig.Jobs.Length <= 0)
      {
        Debug.WriteLine("No Jobs found in Config file. Clearing Jobs.");
      }

      var existingJobs = Context.Jobs.ToList();

      Debug.WriteLine("Synchronizing Jobs with Config...");
      Debug.WriteLine("Deleting jobs not in Config...");
      foreach (var job in existingJobs)
      {
        var jobInConfig = parsedConfig.Jobs.ToList().Find(j => j.Title == job.Title);

        if (jobInConfig == null)
        {
          Context.Jobs.Remove(job);
        }
      }

      if (Context.Jobs.Count() == 0)
      {
        Debug.WriteLine("Empty Job table creating jobs from config");
        foreach (var parsedConfigJob in parsedConfig.Jobs)
        {
          var newJob = new Job()
          {
            Title = parsedConfigJob.Title,
            Uuid = Guid.NewGuid().ToString(),
          };

          Context.Jobs.Add(newJob);
          foreach (var rank in parsedConfigJob.Grades)
          {
            var newRank = new JobRank()
            {
              JobId = newJob.Uuid,
              Salary = rank.Salary,
              Title = rank.Title,
              Uuid = Guid.NewGuid().ToString()
            };
            Context.JobRanks.Add(newRank);
          }
          
        }

        Context.SaveChanges();
      }
      else
      {
        Debug.WriteLine("Updating existing jobs with values from Config...");
        foreach (var parsedConfigJob in parsedConfig.Jobs)
        {
          var job = Context.Jobs.First(j => j.Title == parsedConfigJob.Title);

          if (job != null)
          {
            Debug.WriteLine("Found existing job. Updating...");
            job.Title = parsedConfigJob.Title;

            var configRanks = parsedConfigJob.Grades.ToList();

            var jobRanks = Context.JobRanks.Where(r => r.JobId == job.Uuid).ToList();

            foreach (var configRank in configRanks)
            {
              var oldRank = jobRanks.Find(r => r.Title == configRank.Title);
              if (oldRank != null)
              {
                oldRank.Salary = configRank.Salary;
              }
              else
              {
                var newRank = new JobRank()
                {
                  Uuid = Guid.NewGuid().ToString(),
                  JobId = job.Uuid,
                  Salary = configRank.Salary,
                  Title = configRank.Title
                };
                Context.JobRanks.Add(newRank);
              }
            }
          }
          else
          {
            Debug.WriteLine("Found new Job: {0}", parsedConfigJob.Title);
            var newJob = new Job
            {
              Uuid = Guid.NewGuid().ToString(),
              Title = parsedConfigJob.Title
            };

            Context.Jobs.Add(newJob);
            Context.SaveChanges();

            var jobUuid = Context.Jobs.First(j => j.Title == parsedConfigJob.Title).Uuid;

            foreach (var jobGrade in parsedConfigJob.Grades)
            {
              var newGrade = new JobRank()
              {
                Uuid = Guid.NewGuid().ToString(),
                JobId = jobUuid,
                Title = jobGrade.Title,
                Salary = jobGrade.Salary
              };

              Context.JobRanks.Add(newGrade);
            }

            Context.SaveChanges();
            Debug.WriteLine("New Job saved into Database.");
          }
        }
      }
    }
  }
}