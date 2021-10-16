using System.Data.Entity;
using System.Linq;
using CityOfMindDatabase.Contexts;
using CityOfMindUtils.Utils;
using FiveMForge.Models;

namespace FiveMForge.Database
{
  public class CoreContext : Context
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

    public bool PoiExists(Poi point)
    {
      return Poi.FirstOrDefault(p =>
        Float.FloatEquals(p.X, point.X, 0.01f) &&
        Float.FloatEquals(p.Y, point.Y, 0.01f) &&
        Float.FloatEquals(p.Z, point.Z, 0.01f)
      ) != null;
    }

    public CoreContext(string connectionString) : base(connectionString)
    {
      System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<CoreContext, Configuration>());
    }
  }
}