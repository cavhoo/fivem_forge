using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MySqlConnector;
using Npgsql;

namespace FiveMForge.Database
{
    public class DatabaseConfiguration : DbConfiguration
    {
        public DatabaseConfiguration()
        {
            const string name = "mysql";
            SetProviderFactory(providerInvariantName: name, NpgsqlFactory.Instance);
            SetProviderServices(providerInvariantName: name, provider: NpgsqlServices.Instance);
            SetDefaultConnectionFactory(connectionFactory: new NpgsqlConnectionFactory());
        }
    }
}