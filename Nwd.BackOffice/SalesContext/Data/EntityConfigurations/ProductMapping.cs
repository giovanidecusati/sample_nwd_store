using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nwd.BackOffice.SalesContext.Entities;

namespace Nwd.BackOffice.SalesContext.Data.EntityConfigurations
{
    internal class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", "sales");

            builder.Ignore(p => p.Notifications);
        }
    }
}
