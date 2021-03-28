using AutoMapper;
using FluentValidation.AspNetCore;
using IranTimeFlow.WebApp.Persistance;
using IranTimeFlow.WebApp.Profiles;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IranTimeFlow.WebApp
{
    public static class ServiceInstaller
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddMapper();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddFluentValidatoreCore();
            services.AddScoped<IRepository, Repository>();
            return services;
        }

        private static void AddFluentValidatoreCore(this IServiceCollection services)
        {
            services.AddRazorPages()
                .AddFluentValidation(cfg =>
                {
                    cfg.RegisterValidatorsFromAssemblyContaining(typeof(ServiceInstaller));
                    cfg.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                })
                .AddRazorRuntimeCompilation();
        }

        private static void AddMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiler>();
            });
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
