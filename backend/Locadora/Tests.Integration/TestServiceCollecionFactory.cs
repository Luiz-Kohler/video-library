using Application;
using Domain;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;

namespace Tests.Integration
{
    public static class TestServiceCollecionFactory
    {
        public static IServiceCollection BuildIntegrationTestInfrastructure(string testDatabaseName)
        {
            var services = new ServiceCollection();
            services.AddTestDatabase(testDatabaseName);
            services.AddTestLogs();
            services.AddDomain();
            services.AddApplication();
            services.AddInfrastructure();
            services.AddLogging();
            ConfigureCulture();

            return services;
        }


        private static void ConfigureCulture()
        {
            var ptBrCulture = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = ptBrCulture;
            CultureInfo.DefaultThreadCurrentUICulture = ptBrCulture;
        }
    }
}
