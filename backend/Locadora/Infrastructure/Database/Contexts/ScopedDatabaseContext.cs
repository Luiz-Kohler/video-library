namespace Infrastructure.Database.Contexts
{
    public class ScopedDatabaseContext : IScopedDatabaseContext
    {
        public DatabaseContext Context { get; }

        public ScopedDatabaseContext(DatabaseContext context)
        {
            Context = context;
        }
    }
}
