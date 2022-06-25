using Infrastructure.Environments;
using MySql.Data.MySqlClient;

namespace Infrastructure.Database.Common
{
    public class ConnectionStringFactory : IConnectionStringFactory
    {
        public string GetConnectionString()
        {
            return new MySqlConnectionStringBuilder
            {
                Server = EnvironmentVariables.MySqlServer,
                Database = EnvironmentVariables.MySqlDatabase,
                UserID = EnvironmentVariables.MySqlUser,
                Password = EnvironmentVariables.MySqlPassword,
                Port = EnvironmentVariables.MySqlPort,
            }.ToString();
        }
    }
}
