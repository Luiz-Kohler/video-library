namespace Infrastructure.Environments
{
    public static class EnvironmentVariables
    {
        public static string TimeZone => Environment.GetEnvironmentVariable("timezone") ?? "E. South America Standard Time";
        public static string MySqlServer => Environment.GetEnvironmentVariable("mysql_server") ?? "localhost";
        public static string MySqlPort => Environment.GetEnvironmentVariable("mysql_port") ?? "3306";
        public static string MySqlDatabase => Environment.GetEnvironmentVariable("mysql_database") ?? "locadora";
        public static string MySqlUser => Environment.GetEnvironmentVariable("mysql_user") ?? "root";
        public static string MySqlPassword => Environment.GetEnvironmentVariable("mysql_password") ?? "Root@2022";
        public static bool MySqlPersistSecurityInfo => bool.Parse(Environment.GetEnvironmentVariable("mysql_persist_security_info") ?? "False");
        public static uint MySqlConnectTimeout => uint.Parse(Environment.GetEnvironmentVariable("mysql_connect_timeout") ?? "300");
    }
}