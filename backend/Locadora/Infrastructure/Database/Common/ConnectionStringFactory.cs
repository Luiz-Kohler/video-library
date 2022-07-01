using Domain.Common.Environments;
using MySql.Data.MySqlClient;

namespace Infrastructure.Database.Common
{
    public class ConnectionStringFactory : IConnectionStringFactory
    {
        private readonly IEnvironmentVariables _environmentVariables;
        public ConnectionStringFactory(IEnvironmentVariables environmentVariables)
        {
            _environmentVariables = environmentVariables;
        }

        public string GetConnectionString()
        {
            return new MySqlConnectionStringBuilder
            {
                Server = _environmentVariables.GetEnvironmentVariable(EnvironmentVariablesNames.MySqlServer) ?? "host.docker.internal",
                Database = _environmentVariables.GetEnvironmentVariable(EnvironmentVariablesNames.MySqlDatabase) ?? "locadora",
                UserID = _environmentVariables.GetEnvironmentVariable(EnvironmentVariablesNames.MySqlUser) ?? "root",
                Password = _environmentVariables.GetEnvironmentVariable(EnvironmentVariablesNames.MySqlPassword) ?? "Root@2022",
                Port = uint.Parse(_environmentVariables.GetEnvironmentVariable(EnvironmentVariablesNames.MySqlPort) ?? "3306"),
            }.ToString();
        }
    }
}
