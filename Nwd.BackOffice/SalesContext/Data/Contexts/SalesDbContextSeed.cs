using Microsoft.Extensions.Logging;
using Nwd.BackOffice.SalesContext.Entities;
using System.Threading.Tasks;

namespace Nwd.BackOffice.SalesContext.Data.Contexts
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
