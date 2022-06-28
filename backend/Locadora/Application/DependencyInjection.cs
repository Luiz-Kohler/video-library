using Application.Common.AutoMapper;
using Application.Common.Csv;
using Application.Common.UnitOfWork;
using Application.Common.Validation;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<ICsvService, CsvService>();

            services.AddScoped<IRollbackActionsExecuter, RollbackActionsExecuter>();

            services.AddFluentValidation(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());



            services.AddMiddlewares();
            services.AddAutoMapper();
        }
        private static void AddMiddlewares(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
        }

        private static void AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ClienteMap());
                mc.AddProfile(new FilmeMap());
                mc.AddProfile(new LocacaoMap());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private static void AddFluentValidation(this IServiceCollection services, Assembly assembly)
        {
            var validatorType = typeof(IValidator<>);

            var validatorTypes = assembly
                .GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == validatorType))
                .ToList();

            foreach (var validator in validatorTypes)
            {
                var requestType = validator.GetInterfaces()
                    .Where(i => i.IsGenericType &&
                                i.GetGenericTypeDefinition() == typeof(IValidator<>))
                    .Select(i => i.GetGenericArguments()[0])
                    .First();

                var validatorInterface = validatorType
                    .MakeGenericType(requestType);

                services.AddTransient(validatorInterface, validator);
            }
        }
    }
}
