using Microsoft.Extensions.Logging;
using BackOffice.Sales.Data.Entities;
using System.Threading.Tasks;

namespace BackOffice.Sales.Data.Contexts
{
    public static class SalesDbContextSeed
    {
        public static async Task SeedAsync(SalesDbContext context, ILogger<SalesDbContext> logger)
        {
            using (context)
            {
                logger.LogInformation($"Applying Seed for {nameof(Product)}.");

                await context.SeedProductsAsync();

                await context.SaveChangesAsync();
            }
        }
    }
}
