using System.Linq;
using CitizenFX.Core;
using FiveMForge.Database;
using FiveMForge.Database.Contexts;
using FiveMForge.Database.Models;
using FiveMForge.Models;

namespace FiveMForge.Controller.Base
{
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
          //DbInit.CreateTables();
          using (var ctx = new CoreContext())
          {
              foreach (var atmLocation in AtmLocations.Locations)
              {
                  var atm = new Atm();
                  atm.Location = atmLocation.ToString();
                  ctx.Atms.Add(atm);
              }

              ctx.SaveChanges();
          }
      }
    }

}
