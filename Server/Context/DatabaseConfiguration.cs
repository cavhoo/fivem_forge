using System.Data.Entity;
using Npgsql;

namespace Server.Context
{
    public class DatabaseConfiguration : DbConfiguration
    {
        public DatabaseConfiguration()
        {
            const string name = "mysql";
            SetProviderFactory(providerInvariantName: name, NpgsqlFactory.Instance);
            SetProviderServices(providerInvariantName: name, provider: NpgsqlServices.Instance);
            SetDefaultConnectionFactory(connectionFactory: new NpgsqlConnectionFactory());
            SetMigrationSqlGenerator(name, () => new NpgsqlMigrationSqlGenerator());
        }
    }
}