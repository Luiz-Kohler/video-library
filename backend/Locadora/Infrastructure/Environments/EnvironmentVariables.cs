namespace Infrastructure.Environments
{
    public static class EnvironmentVariables
    {
        public static string TimeZone => Environment.GetEnvironmentVariable("timezone") ?? "E. South America Standard Time";
        public static string MySqlServer => Environment.GetEnvironmentVariable("mysql_server") ?? "host.docker.internal";
        public static uint MySqlPort => uint.Parse(Environment.GetEnvironmentVariable("mysql_port") ?? "3306");
        public static string MySqlDatabase => Environment.GetEnvironmentVariable("mysql_database") ?? "locadora";
        public static string MySqlUser => Environment.GetEnvironmentVariable("mysql_user") ?? "root";
        public static string MySqlPassword => Environment.GetEnvironmentVariable("mysql_password") ?? "Root@2022";
    }
}