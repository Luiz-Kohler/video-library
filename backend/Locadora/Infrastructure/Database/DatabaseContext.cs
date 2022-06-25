using Infrastructure.Database.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var mappingTypes = typeof(BaseMapping<>).Assembly
                .GetTypes()
                .Where(t => t.IsAssignableTo(typeof(IBaseMapping)))
                .Where(t => t.IsAbstract is false)
                .ToList();

            mappingTypes.ForEach(mappingType =>
            {
                var mapping = Activator.CreateInstance(mappingType);
                var initializeMethod = mapping.GetType().GetMethod(nameof(IBaseMapping.MapearEntidade));
                initializeMethod.Invoke(mapping, new object[] { modelBuilder });
            });
        }
    }
}
