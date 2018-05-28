using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackOffice.Sales.Data.Entities;

namespace BackOffice.Sales.Data.EntityConfigurations
{
    internal class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", "sales");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ForSqlServerUseSequenceHiLo("SEQ_Products");

            builder.Property(p => p.Name)
                .HasColumnType("varchar(128)");

            builder.HasOne(p => p.Category);

            builder.Ignore(p => p.Notifications);
        }
    }
}
