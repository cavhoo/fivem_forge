using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CitizenFX.Core;
using CityOfMindDatabase.Contexts;
using CityOfMindJobs.Context;
using CityOfMindPluginBase;
using Newtonsoft.Json;

namespace CityOfMindJobs
{
  public class CityOfMindJobs : IPlugin
  {
    protected JobContext Context;
    protected IPluginApi Api;

    public async void Start(string connectionString, IPluginApi pluginApi)
    {
      Debug.WriteLine("Starting plugin Jobs");
      Context = new JobContext(connectionString);
      Api = pluginApi;
      CreateUnemployedJob();
      CreateCarMechanicJob();
      await Context.SaveChangesAsync();

      Api.RegisterEvent("CityOfMind:CreatePayments", CreatePayments);
      Api.RegisterEvent("CityOfMind:AssignJobToCharacter", AssingJobToCharacter);
    }

    private void AssingJobToCharacter([FromSource] Player player, string jsonData)
    {
      var parsedData = JsonConvert.DeserializeObject<IDictionary<string, string>>(jsonData);
      if (parsedData != null)
      {
        var characterUuid = parsedData["characterUuid"];
        var jobUuid = parsedData["jobUuid"];
        var existingJob =
          Context.CharacterJobs.FirstOrDefault(cj => cj.CharacterUuid == characterUuid);
        if (existingJob != null)
        {
          existingJob.JobUuid = jobUuid;
        }

        var newJob = new CharacterJob();
        newJob.JobUuid = jobUuid;
        newJob.CharacterUuid = characterUuid;
        Context.CharacterJobs.Add(new CharacterJob());
      }
    }

    private void CreatePayments([FromSource] Player player, string jsonData)
    {
    }

    protected void CreateUnemployedJob()
    {
      var unemployedJob = new Job();
      unemployedJob.Title = "job.unemployed";
      unemployedJob.AvailableRanks = new List<JobRank>();
      unemployedJob.AvailableRanks.Add(new JobRank("job.unemployed.rank.base", 400, null, null));
      Context.Jobs.Add(unemployedJob);
    }

    protected void CreateCarMechanicJob()
    {
      var mechanicJob = new Job();
      mechanicJob.Title = "job.carmechanic";
      mechanicJob.AvailableRanks = new List<JobRank>();
      var job1 = new JobRank("job.carMechanic.rank.mechanic", 1500, null, null);
      mechanicJob.AvailableRanks.Add(job1);
      var job2 = new JobRank("job.carMechanic.rank.superviser", 3000, null, null);
      mechanicJob.AvailableRanks.Add(job2);
      mechanicJob.AvailableRanks.Add(new JobRank("job.carMechanic.rank.boss", 5000, null, null));

      Context.Jobs.Add(mechanicJob);
    }
  }
}