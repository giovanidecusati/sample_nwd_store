using BackOffice.Sales.Data.Entities;
using BackOffice.Sales.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BackOffice.Sales.Data.Contexts
{
    public class SalesDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductMapping());
            builder.ApplyConfiguration(new CategoryMapping());
        }
    }

    /// <summary>
    /// For local development purpose. EF use to apply migration using c-line.
    /// Message: Unable to create an object of type 'IntegrationEventLogContext'. 
    /// Add an implementation of 'IDesignTimeDbContextFactory<IntegrationEventLogContext>' to the project, 
    /// or see https://go.microsoft.com/fwlink/?linkid=851728 for additional patterns supported at design time.
    /// </summary>
    public class BackOfficeContextDesignFactory : IDesignTimeDbContextFactory<SalesDbContext>
    {
        public SalesDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SalesDbContext>()
                .UseSqlServer("Server=(local);Database=BackOfficeDb;User id=sa;Password=12qwaszx@@34;MultipleActiveResultSets=true");

            return new SalesDbContext(optionsBuilder.Options);
        }
    }
}
