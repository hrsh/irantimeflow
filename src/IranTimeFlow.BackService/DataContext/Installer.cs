using EasyCaching.InMemory;
using EFCoreSecondLevelCacheInterceptor;
using IranTimeFlow.BackService.DataContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text;

namespace IranTimeFlow.BackService.DataContext
{
    public static class Installer
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment)
        {
            var connectionstring = configuration.MakeConnectionString();
            services.AddDbContextPool<AppDbContext>((serviceProvider, optionBuilder) =>
            {
                optionBuilder
                .UseSqlServer(connectionstring)
                .UseLazyLoadingProxies()
                .EnableSensitiveDataLogging(webHostEnvironment.IsDevelopment())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

                optionBuilder.AddInterceptors(serviceProvider.GetRequiredService<SecondLevelCacheInterceptor>());
            });

            services.AddEasyCaching();

            return services;
        }

        public static string MakeConnectionString(
            this IConfiguration configuration)
        {
            var host = configuration["DbParam_Host"];
            var database = configuration["DbParam_Database"];
            var username = configuration["DbParam_Username"];

            // just for fun :)
            var password = configuration["DbParam_Password"];
            var pass = Encoding.UTF8.GetString(Convert.FromBase64String(password));

            return $"Data Source={host};Initial Catalog={database};User ID={username};Password={pass};MultipleActiveResultSets=true";
        }

        private static void AddEasyCaching(this IServiceCollection services)
        {
            services.AddEFSecondLevelCache(opt =>
            {
                opt.UseEasyCachingCoreProvider("default")
                .DisableLogging(false);
            });

            services.AddEasyCaching(opt =>
            {
                opt.UseInMemory(config =>
                {
                    config.DBConfig = new InMemoryCachingOptions
                    {
                        ExpirationScanFrequency = 60,
                        SizeLimit = 100,
                        EnableReadDeepClone = true,
                        EnableWriteDeepClone = true
                    };
                    config.EnableLogging = true;
                    config.MaxRdSecond = 10;
                }, "default");
            });
        }
    }
}
