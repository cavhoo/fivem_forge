using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace CityOfMindDatabase.Contexts
{
    public class Context : DbContext
    {
        public Context(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Disable plurality table names.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
    
}
