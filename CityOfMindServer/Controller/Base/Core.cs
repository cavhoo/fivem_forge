using System.Linq;
using CitizenFX.Core;
using FiveMForge.Database.Contexts;
using FiveMForge.Models;

namespace FiveMForge.Controller.Base
{
    /// <summary>
    /// Class <c>Core</c> is the entry point into the server.
    /// This class ensures that the database is initialized and
    /// populated with the default values needed to run the server.
    /// </summary>
    public class Core : BaseScript
    {
        public Core()
        {
            this.InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            Debug.WriteLine("FiveM Forge Starting...verifying database...");
            Debug.WriteLine("Checking if all tables exist, if not then we create them :)");
            Debug.WriteLine("Roflcopter");
            //DbInit.CreateTables();
            using (var ctx = new CoreContext())
            {
                var atms = ctx.Atms.ToList();
                if (atms.Count == 0)
                {
                    Debug.WriteLine("No ATM Data found, populating table....");
                    foreach (var atmLocation in AtmLocations.Locations)
                    {
                        var atm = new Atm();
                        atm.Location = atmLocation.ToString();
                        ctx.Atms.Add(atm);
                    }
                }

                var banks = ctx.Banks.ToList();
                if (banks.Count == 0)
                {
                    Debug.WriteLine("No Bank Data found, populating table...");
                    foreach (var bankLocation in BankLocations.Locations)
                    {
                        var bank = new Bank();
                        bank.Name = bankLocation.Name;
                        bank.IsActive = bankLocation.IsActive;
                        bank.IsAdminOnly = bankLocation.IsAdminOnly;
                        bank.Location = $"{bankLocation.X}:{bankLocation.Y}:{bankLocation.Z}";
                        ctx.Banks.Add(bank);
                    }
                }

                var tiers = ctx.Tiers.ToList();

                if (tiers.Count == 0)
                {
                    Debug.WriteLine("No Tier data found, populating table...");
                    ctx.Tiers.Add(TiersTemplates.Admin);
                    ctx.Tiers.Add(TiersTemplates.Common);
                    ctx.Tiers.Add(TiersTemplates.Owner);
                    ctx.Tiers.Add(TiersTemplates.Supporter);
                    ctx.Tiers.Add(TiersTemplates.Vip);
                }

                ctx.SaveChanges();
            }
        }
    }
}