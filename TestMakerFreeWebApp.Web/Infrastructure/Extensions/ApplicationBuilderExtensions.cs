using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestMakerFreeWebApp.Data;
using TestMakerFreeWebApp.Data.DataSeed;

namespace TestMakerFreeWebApp.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IWebHost MigrateAndSeedDatabase(this IWebHost webhost)
        {
            using (var serviceScope = webhost.Services.CreateScope().ServiceProvider.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                DbSeeder.Seed(dbContext);
            }

            return webhost;
        }
    }
}
