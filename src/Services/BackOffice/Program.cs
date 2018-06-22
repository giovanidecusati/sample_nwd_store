using BackOffice.Sales.Data.Contexts;
using BuildingBlock.IntegrationEventLog;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Sales
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            host.MigrateDbContext<IntegrationEventLogContext>((integrationEventLogContext, logger) =>
            {
                integrationEventLogContext.Database.EnsureCreated();
                integrationEventLogContext.Database.MigrateAsync().Wait();
            });

            host.MigrateDbContext<SalesDbContext>((salesContext, logger) =>
            {
                salesContext.Database.Migrate();
                SalesDbContextSeed.SeedAsync(salesContext, logger).Wait();
            });

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
