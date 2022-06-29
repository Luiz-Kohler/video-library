using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Precos.GovernancaPrecos.Presentation;

namespace Web.Common.Extensions
{
    public static class WebHostExtensions
    {
        public static void RunMigration(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var logger = host.Services.GetService<ILogger<Program>>();
                logger.LogInformation("Aplicando Migration.");

                var db = scope.ServiceProvider.GetService<DatabaseContext>();
                db.Database.Migrate();

                logger.LogInformation("Migration aplicada com sucesso.");
            }
        }
    }
}
