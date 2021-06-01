using IranTimeFlow.WebApp.DataContext;
using IranTimeFlow.WebApp.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace IranTimeFlow.WebApp
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
            services.AddInfrastructure(_configuration, _webHostEnvironment.IsDevelopment());
            services.AddCustomServices(opt => opt.WebAppPolicyOptions());

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
            services
                .AddResponseCompression(config =>
                {
                    config.Providers.Add<BrotliCompressionProvider>();
                    config.Providers.Add<GzipCompressionProvider>();
                    config.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                            new[] { "image/svg+xml" }
                        );
                });
            services.AddAntiforgery(config => { config.HeaderName = "XSRF-TOKEN"; });
            services.AddHttpsRedirection(cfg =>
            {
                cfg.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                cfg.HttpsPort = 443;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
