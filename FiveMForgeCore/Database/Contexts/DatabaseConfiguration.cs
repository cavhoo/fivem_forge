using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations.Sql;
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
            SetMigrationSqlGenerator(name, () => new NpgsqlMigrationSqlGenerator());
        }
    }
}