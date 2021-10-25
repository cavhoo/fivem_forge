using System.Data.Entity.Migrations;

namespace CityOfMindJobs.Context
{
    internal sealed class Configuration: DbMigrationsConfiguration<JobContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            // TODO: Remove this for production to prevent screwing up the database.
            AutomaticMigrationDataLossAllowed = false;
            ContextKey = "JobContext";
        }
    }
}