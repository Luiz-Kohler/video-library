﻿using Application;
using Application.Common.Exceptions;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using System.Globalization;

namespace Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddDomain();
            services.AddInfrastructure();

            services.AddControllers();

            services.AddExceptionHandler(options =>
            {
                options.ExceptionHandler = GlobalExceptionHandler.Handle;
                options.AllowStatusCode404Response = true;
            });

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = 1024 * 1024 * 1024; 
                x.MultipartBodyLengthLimit = 1024 * 1024 * 1024; 
            });

            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(option => option
                .SetIsOriginAllowed(_ => true)
                .AllowAnyHeader()
                .WithMethods("POST", "PUT", "DELETE", "GET")
                .AllowCredentials()
            );

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ConfigureCulture();
        }

        private static void ConfigureCulture()
        {
            var ptBrCulture = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = ptBrCulture;
            CultureInfo.DefaultThreadCurrentUICulture = ptBrCulture;
        }
    }
}
