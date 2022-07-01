using Domain.Common.Environments;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddSingleton<IEnvironmentVariables, EnvironmentVariables>();
            return services;
        }
    }
}
