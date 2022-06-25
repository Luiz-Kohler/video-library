namespace Infrastructure.Database.Contexts
{
    public interface IScopedDatabaseContext
    {
        DatabaseContext Context { get; }
    }
}
