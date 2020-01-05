using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(auth.Areas.Identity.IdentityHostingStartup))]
namespace auth.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}