using AutoMapper;
using FluentValidation.AspNetCore;
using IranTimeFlow.WebApp.DataContext;
using IranTimeFlow.WebApp.Helpers;
using IranTimeFlow.WebApp.Persistance;
using IranTimeFlow.WebApp.Profiles;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace IranTimeFlow.WebApp
{
    public static class ServiceInstaller
    {
        public static IServiceCollection AddCustomServices(
            this IServiceCollection services,
            Action<RazorPagesOptions> options)
        {
            services.AddRouting(cfg => cfg.LowercaseUrls = true);
            services.AddMapper();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddCustomRazorPages(options);
            services.AddControllers();
            services.AddScoped<IRepository, Repository>();
            services.AddCustomIdentity();
            return services;
        }

        private static void AddCustomRazorPages(
            this IServiceCollection services,
            Action<RazorPagesOptions> options)
        {
            services
            .AddRazorPages().AddRazorPagesOptions(options)
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

        private static void AddCustomIdentity(this IServiceCollection services)
        {
            services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie()
            .AddGoogle(cfg =>
            {
                cfg.ClientId = "***.apps.googleusercontent.com";
                cfg.ClientSecret = "***";
            })
            .AddMicrosoftAccount(cfg =>
            {
                cfg.ClientId = "***";
                cfg.ClientSecret = "***";
            });
            services.AddIdentity<IdentityUser, IdentityRole>(cfg =>
            {
                cfg.Password.RequireDigit = true;
                cfg.SignIn.RequireConfirmedEmail = true;
                cfg.SignIn.RequireConfirmedPhoneNumber = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
            services.AddAuthorization(cfg =>
            {
                cfg.AddCustomPolicies();
            });
            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/account/u/login");
                opt.Cookie.Name = "_auth";
            });
        }
    }
}
