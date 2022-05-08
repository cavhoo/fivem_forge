using System;
using System.Linq;
using CitizenFX.Core;
using Common.Models;
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
      Action<Player, string, object[]> clientEventTriggerFunc, Action<string, object[]> clientEventTriggerAllFunc) : base(handlers, eventTriggerFunc,
      clientEventTriggerFunc, clientEventTriggerAllFunc)
    {
      EventHandlers[JobEvents.RegisterJob] += new Action<string>(OnRegisterJob);
      EventHandlers[JobEvents.CreateRank] += new Action<Player, string, string, int>(OnCreateRank);
      EventHandlers[JobEvents.RenameRank] += new Action<Player, string, string>(OnRenameRank);
      EventHandlers[JobEvents.RemoveRank] += new Action<Player, string>(OnRemoveRank);
      LoadJobConfigFile();
    }

    private async void OnRegisterJob(string jobTitle)
    {
      var jobExists = Context.Jobs.FirstOrDefault(j => j.Title == jobTitle);

      if (jobExists != null)
      {
        TriggerEvent(JobEvents.JobAlreadyExists, jobTitle);
        return;
      }

      // No job found so we can register a new job.
      var newJob = new Job()
      {
        Title = jobTitle,
        Uuid = Guid.NewGuid().ToString(),
      };

      Context.Jobs.Add(newJob);
      await Context.SaveChangesAsync();
      // Fire event to the script that created the job to inform them about
      // the job uuid. Title is used for filtering inside the job modules.
      TriggerEvent(JobEvents.JobRegistered, jobTitle, newJob.Uuid);
    }

    private async void OnCreateRank([FromSource] Player player, string jobId, string rankTitle, int rankSalary)
    {
      var rankExists = Context.JobRanks.FirstOrDefault(r => r.Title == rankTitle);

      if (rankExists != null)
      {
        TriggerEvent(JobEvents.RankAlreadyExists, rankTitle);
        return;
      }

      var newRank = new JobRank()
      {
        JobId = jobId,
        Title = rankTitle,
        Salary = rankSalary,
        Uuid = Guid.NewGuid().ToString()
      };

      Context.JobRanks.Add(newRank);
      await Context.SaveChangesAsync();
      player.TriggerEvent(JobEvents.RankCreated);
    }

    private async void OnRenameRank([FromSource] Player player, string rankId, string newName)
    {
      var rank = Context.JobRanks.FirstOrDefault(r => r.Uuid == rankId);

      if (rank == null)
      {
        // We should never end up here!
        player.TriggerEvent(JobEvents.RankDoesNotExist);
        return;
      }

      rank.Title = newName;

      await Context.SaveChangesAsync();
      // Dispatch that the name has been changed.
      TriggerClientEvent(JobEvents.RankRenamed, rankId, newName);
    }

    private async void OnRemoveRank([FromSource] Player player, string rankId)
    {
      var rankToRemove = Context.JobRanks.FirstOrDefault(r => r.Uuid == rankId);

      if (rankToRemove == null)
      {
        // We should not end up here.
        player.TriggerEvent(JobEvents.RankDoesNotExist);
      }
      else
      {
        var charactersWithRank = Context.Characters.Where(c => c.JobUuid == rankId).ToList();

        if (charactersWithRank.Count > 0)
        {
          // If we have characters with this Jobrank they need to be changed to a different
          // Rank before a rank can be removed.
          player.TriggerEvent(JobEvents.RankInUse, rankId);
          return;
        }
        
        Context.JobRanks.Remove(rankToRemove);
        await Context.SaveChangesAsync();
      }
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

      // This job is always present.
      var unemployedJob = Context.Jobs.First(job => job.Title == "Arbeitslos");
      var unemployedRank = Context.JobRanks.First(jobRank => jobRank.JobId == unemployedJob.Uuid);

      // Get all jobs that are not 'Arbeitslos'
      var existingJobs = Context.Jobs.Where(job => job.Title != "Arbeitslos").ToList();

      Debug.WriteLine("Synchronizing Jobs with Config...");
      Debug.WriteLine("Deleting jobs not in Config...");
      foreach (var job in existingJobs)
      {
        // Check if the job is inside the config.
        var jobInConfig = parsedConfig.Jobs.ToList().Find(j => j.Title == job.Title);

        // If the job is not part of the config schedule it for deletion.
        if (jobInConfig == null)
        {
          // Get all ranks assigned to the job.
          var oldJobRanks = Context.JobRanks.Where(rank => rank.JobId == job.Uuid);

          // Get all characters that have the job that's being deleted.
          var charactersWithJobs = Context.Characters.Where(c => oldJobRanks.Contains(c.Job)).ToList();

          // Set all characters to unemployed that had a now deleted job.
          foreach (var character in charactersWithJobs)
          {
            character.JobUuid = unemployedRank.JobId;
          }

          // Remove all the job ranks not needed anymore.
          Context.JobRanks.RemoveRange(oldJobRanks);

          // Remove the job.
          Context.Jobs.Remove(job);
        }
      }

      Context.SaveChanges();

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

          CreateBankAccount(newJob.Uuid);

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
          Debug.WriteLine($"Updating job {parsedConfigJob.Title}");
          var job = Context.Jobs.FirstOrDefault(j => j.Title == parsedConfigJob.Title);

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
                Debug.WriteLine("Found existing rank, updating salary...");
                oldRank.Salary = configRank.Salary;
              }
              else
              {
                Debug.WriteLine("New job rank found. Adding...");
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

            Context.SaveChanges();
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

            CreateBankAccount(newJob.Uuid);


            foreach (var jobGrade in parsedConfigJob.Grades)
            {
              var newGrade = new JobRank()
              {
                Uuid = Guid.NewGuid().ToString(),
                JobId = newJob.Uuid,
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

    private void CreateBankAccount(string jobUuid)
    {
      // Create Bankaccount for Job
      var rand = new Random();

      var firstTriple = rand.Next(1000);
      var secondTriple = rand.Next(1000);
      var thirdTriple = rand.Next(1000);
      var accountNumber = $"{firstTriple}{secondTriple}{thirdTriple}";

      var companyBankAccount = new BankAccount()
      {
        Holder = jobUuid,
        AccountNumber = accountNumber,
        Saldo = ConfigController.GetInstance().Config.InitialBankAmountBalance
      };

      Context.BankAccount.Add(companyBankAccount);
      Context.SaveChanges();
    }
  }
}