using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using TestMakerFreeWebApp.Web.Infrastructure.Extensions;

namespace TestMakerFreeWebApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
                .MigrateAndSeedDatabase()
                .Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(options => options.AddServerHeader = false)
                .UseStartup<Startup>()
                .Build();
    }
}
