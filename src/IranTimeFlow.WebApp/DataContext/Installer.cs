using EasyCaching.InMemory;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IranTimeFlow.WebApp.DataContext
{
    public static class Installer
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration,
            bool logging)
        {
            services.AddDbContextPool<AppDbContext>((serviceProvider, optionBuilder) =>
            {
                optionBuilder.UseSqlServer(configuration.GetConnectionString("iran-time-flow"))
                .UseLazyLoadingProxies()
                .EnableSensitiveDataLogging(logging)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

                optionBuilder.AddInterceptors(serviceProvider.GetRequiredService<SecondLevelCacheInterceptor>());
            });

            services.AddEasyCaching();

            return services;
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
