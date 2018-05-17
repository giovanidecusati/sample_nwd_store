using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Nwd.BackOffice.SalesContext.Data.Contexts;

namespace Nwd.BackOffice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
               .MigrateDbContext<SalesDbContext>((salesContext, logger) =>
               {
                   SalesDbContextSeed.SeedAsync(salesContext, logger).Wait();
               })
              .Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
