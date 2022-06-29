using Web;
using Web.Common.Extensions;

namespace Web.Presentation;

public class Program
{
    protected Program() { }

    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        host.RunMigration();
        host.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    .UseKestrel(serverOptions => { })
                    .UseStartup<Startup>();
            });
}