using CitizenFX.Core;
using FiveMForge.Database;

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
          DbInit.CreateTables();
      }
    }

}
