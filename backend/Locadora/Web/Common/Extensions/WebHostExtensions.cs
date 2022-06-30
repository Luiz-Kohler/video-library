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

                for (var i = 0; i < 6; i++)
                {
                    try
                    {
                        var db = scope.ServiceProvider.GetService<DatabaseContext>();
                        db.Database.Migrate();
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.Write("ERROR AO RODAR MIGRATION: ");
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.InnerException);

                        Thread.Sleep(15000);

                        continue;
                    }
                }

                logger.LogInformation("Migration aplicada com sucesso.");
            }
        }
    }
}
