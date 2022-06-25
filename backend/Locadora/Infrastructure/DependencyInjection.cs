using Application.Common.UnitOfWork;
using Infrastructure.Database;
using Infrastructure.Database.Common;
using Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            AddMySql(services);
            return services;
        }

        private static void AddMySql(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionStringFactory, ConnectionStringFactory>();

            var serviceProvider = services.BuildServiceProvider();
            var connectionString = serviceProvider
                .GetRequiredService<IConnectionStringFactory>()
                .GetConnectionString();

            services
                .AddDbContextPool<DatabaseContext>(options =>
                {
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                });

            services.AddScoped<IScopedDatabaseContext, ScopedDatabaseContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
