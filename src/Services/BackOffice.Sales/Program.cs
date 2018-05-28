using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using BackOffice.Sales.Data.Contexts;

namespace BackOffice.Sales
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
