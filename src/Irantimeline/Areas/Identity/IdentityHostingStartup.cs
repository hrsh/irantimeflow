using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Irantimeline.Areas.Identity.IdentityHostingStartup))]
namespace Irantimeline.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}