using System.Data.Entity.Migrations;

namespace FiveMForge.Database
{
    internal sealed class Configuration: DbMigrationsConfiguration<CoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            // TODO: Remove this for production to prevent screwing up the database.
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "CoreContext";
        }

        protected override void Seed(CoreContext context)
        {
            base.Seed(context);
        }
    }
}