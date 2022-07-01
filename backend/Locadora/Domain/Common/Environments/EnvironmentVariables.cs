namespace Domain.Common.Environments
{
    public class EnvironmentVariables : IEnvironmentVariables
    {
        public string GetEnvironmentVariable(string variableName)
        {
            return System.Environment.GetEnvironmentVariable(variableName);
        }
    }
}
