using System.Data.Entity.Migrations;

namespace FiveMForge.Database.Contexts
{
    internal sealed class Configuration: DbMigrationsConfiguration<CoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "CoreContext";
        }

        protected override void Seed(CoreContext context)
        {
            base.Seed(context);
        }
    }
}