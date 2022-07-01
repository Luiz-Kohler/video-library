namespace Domain.Common.Environments
{
    public interface IEnvironmentVariables
    {
        string GetEnvironmentVariable(string variableName);
    }
}
