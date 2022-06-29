using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Web.Presentation;

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

                try
                {
                    var db = scope.ServiceProvider.GetService<DatabaseContext>();
                    db.Database.Migrate();

                }
                catch (Exception ex)
                {

                }

                logger.LogInformation("Migration aplicada com sucesso.");
            }
        }
    }
}
