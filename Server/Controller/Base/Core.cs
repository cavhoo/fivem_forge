using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using CityOfMindDatabase.Contexts;
using Server.Controller.Config;
using Server.Context;
using Server.Controller.Character;
using Server.Controller.Jobs;
using Server.Controller.Money;
using Server.Controller.Session;
using Server.Controller.Spawn;
using Server.Models;
using static CitizenFX.Core.Native.API;

namespace Server.Controller.Base
{
    /// <summary>
    /// Class <c>Core</c> is the entry point into the server.
    /// This class ensures that the database is initialized and
    /// populated with the default values needed to run the server.
    /// </summary>
    public class Core : BaseScript
    {
        private SessionController SessionController;
        private AtmController AtmController;
        private BankingController BankingController;
        private PaymentController PaymentController;
        private CharacterController CharacterController;
        private SpawnController SpawnController;
        private JobController JobController;
        public Core()
        {
            InitializeDatabase();
            InitializeServer();
            InitializeTick();
        }

        private void InitializeDatabase()
        {
            Debug.WriteLine("FiveM Forge Starting...verifying database...");
            Debug.WriteLine("Checking if all tables exist, if not then we create them :)");
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

        private void InitializeServer()
        {
            Debug.WriteLine("Firing Up City of Mind Server standby...");

            SetGameType("City of Mind");
            SetMapName("San Andreas");
            
            SessionController = new SessionController(EventHandlers, TriggerEvent, TriggerClientEvent);
            AtmController = new AtmController(EventHandlers, TriggerEvent, TriggerClientEvent);
            CharacterController = new CharacterController(EventHandlers, TriggerClientEvent, TriggerClientEvent);
            BankingController = new BankingController(EventHandlers, TriggerEvent, TriggerClientEvent);
            PaymentController = new PaymentController(EventHandlers, TriggerEvent, TriggerClientEvent);
            SpawnController = new SpawnController(EventHandlers, TriggerEvent, TriggerClientEvent);
            JobController = new JobController(EventHandlers, TriggerEvent, TriggerClientEvent);
            Debug.WriteLine("Server initialized...");
        }

        private void InitializeTick()
        {
            Tick += OnServerTick;
        }

        private async Task OnServerTick()
        {
            PaymentController.OnTick();
        }
        
    }
}