using System;
using System.Security.Cryptography.X509Certificates;
using MySqlConnector;

namespace FiveMForge.Database
{
    public class DbConnector : IDisposable
    {
        public readonly MySqlConnection Connection;
        public DbConnector()
        {
            Connection = new MySqlConnection(Constants.Database.ConString);
        }

        public async void Open()
        {
            await Connection.OpenAsync();
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}