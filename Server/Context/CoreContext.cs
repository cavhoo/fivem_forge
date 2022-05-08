using System.Data.Entity;
using System.Linq;
using CityOfMindUtils.Utils;
// using Common.Models;
// using Common.Models.Character;
// using Common.Models.Factions;
// using Common.Models.Jobs;
// using Common.Models.Money;
// using Common.Models.Player;
using Server.Models;
using Server.Controller.Config;
using Server.Models.Character;
using Server.Models.Factions;
using ConfigController = Server.Controller.Config.ConfigController;

namespace Server.Context
{
  public class CoreContext : CityOfMindDatabase.Contexts.Context
  {
    public DbSet<BankAccount> BankAccount { get; set; }
    public DbSet<PendingBankTransaction> PendingBankTransactions { get; set; }
    public DbSet<BankTransaction> BankTransactions { get; set; }
    public DbSet<Money> Money { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Bank> Banks { get; set; }
    public DbSet<Atm> Atms { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Poi> Poi { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Tiers> Tiers { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<JobRank> JobRanks { get; set; }
    public DbSet<Faction> Factions { get; set; }
    public DbSet<FactionRank> FactionRanks { get; set; }
    public DbSet<FactionRankPermissions> FactionRankPermissions { get; set; }

    public bool PoiExists(Poi point)
    {
      return Poi.FirstOrDefault(p =>
        Float.FloatEquals(p.X, point.X, 0.01f) &&
        Float.FloatEquals(p.Y, point.Y, 0.01f) &&
        Float.FloatEquals(p.Z, point.Z, 0.01f)
      ) != null;
    }

    public CoreContext() : base(ConfigController.GetInstance().Config.ConnectionString)
    {
      // System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CoreContext>());
      System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<CoreContext, Configuration>());
    }
  }
}