using AutoMapper;
using IranTimeFlow.BackService.Persistance;
using IranTimeFlow.BackService.Profiles;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IranTimeFlow.BackService
{
    public static class ServiceInstaller
    {
        public static IServiceCollection AddCustomServices(
            this IServiceCollection services)
        {
            services.AddRouting(cfg => cfg.LowercaseUrls = true);
            services.AddMapper();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddCustomRazorPages();
            services.AddControllers();
            services.AddScoped<IRepository, Repository>();
            return services;
        }

        private static void AddCustomRazorPages(
            this IServiceCollection services)
        {
            services
            .AddControllers();
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
