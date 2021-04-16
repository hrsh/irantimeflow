using IranTimeFlow.BackService.DataContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IranTimeFlow.BackService
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Startup(
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment) =>
            (_configuration, _webHostEnvironment) = (configuration, webHostEnvironment);

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(
                _configuration,
                _webHostEnvironment);

            services.AddCustomServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(e =>
            {
                e.MapControllers();
            });
        }
    }
}
