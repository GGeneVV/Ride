using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(RidePal.Web.Areas.Identity.IdentityHostingStartup))]
namespace RidePal.Web.Areas.Identity
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