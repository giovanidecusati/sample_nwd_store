using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuildingBlock.IntegrationEventLog
{
    public class IntegrationEventLogContext : DbContext
    {
        public IntegrationEventLogContext(DbContextOptions<IntegrationEventLogContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<IntegrationEventLogEntry> IntegrationEventLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IntegrationEventLogEntry>(ConfigureIntegrationEventLogEntry);
        }

        void ConfigureIntegrationEventLogEntry(EntityTypeBuilder<IntegrationEventLogEntry> builder)
        {
            builder.ToTable("IntegrationEventLogs", "events");

            builder.HasKey(e => e.EventId);

            builder.Property(e => e.EventId)
                .IsRequired();

            builder.Property(e => e.Content)
                .IsRequired();

            builder.Property(e => e.CreationTime)
                .IsRequired();

            builder.Property(e => e.State)
                .IsRequired();

            builder.Property(e => e.TimesSent)
                .IsRequired();

            builder.Property(e => e.EventTypeName)
                .IsRequired();

        }
    }

    /// <summary>
    /// For local development purpose. EF use to apply migration using c-line.
    /// Message: Unable to create an object of type 'IntegrationEventLogContext'. 
    /// Add an implementation of 'IDesignTimeDbContextFactory<IntegrationEventLogContext>' to the project, 
    /// or see https://go.microsoft.com/fwlink/?linkid=851728 for additional patterns supported at design time.
    /// </summary>
    public class IntegrationEventLogContextDesignFactory : IDesignTimeDbContextFactory<IntegrationEventLogContext>
    {
        public IntegrationEventLogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IntegrationEventLogContext>()
                .UseSqlServer("Server=(local);Database=BackOfficeDb;User id=sa;Password=12qwaszx@@34;MultipleActiveResultSets=true");

            return new IntegrationEventLogContext(optionsBuilder.Options);
        }
    }

}
