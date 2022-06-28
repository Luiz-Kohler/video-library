using Application.Common.UnitOfWork;
using Domain.IRepositories;
using Infrastructure.Database;
using Infrastructure.Database.Common;
using Infrastructure.Database.Contexts;
using Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            AddRepositories(services);
            AddMySql(services);
            return services;
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IClientesRepository, ClientesRepository>();
            services.AddScoped<IFilmesRepository, FilmesRepository>();
            services.AddScoped<ILocacoesRepository, LocacoesRepository>();
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
