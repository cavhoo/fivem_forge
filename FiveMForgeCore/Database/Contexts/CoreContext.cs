using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using FiveMForge.Database.Models;

namespace FiveMForge.Database.Contexts
{
    public class CoreContext : DbContext
    {
        public CoreContext() : base(Constants.Database.ConStringEF)
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<CoreContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Disable plurality table names.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // Postgres uses 'public' as schema.
            //modelBuilder.HasDefaultSchema("public");
        }

        public DbSet<BankAccount> BankAccount { get; set; }
        public DbSet<PendingBankTransaction> PendingBankTransactions { get; set; }
        public DbSet<BankTransaction> BankTransactions { get; set; }
        public DbSet<Money> Money { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Atm> Atms { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Poi> Poi { get; set; }
    }
}
