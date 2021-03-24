using System;
using System.Security.Cryptography.X509Certificates;
using MySqlConnector;

namespace FiveMForge.database
{
    public class DbConnector : IDisposable
    {
        public readonly MySqlConnection Connection;
        public DbConnector()
        {
            Connection = new MySqlConnection("host=localhost;port=3306;user id=root;password=;database=forge;");
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