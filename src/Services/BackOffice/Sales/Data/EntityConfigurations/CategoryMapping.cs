using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackOffice.Sales.Data.Entities;

namespace BackOffice.Sales.Data.EntityConfigurations
{
    internal class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories", "sales");

            builder.HasKey(p => p.Id)
                .ForSqlServerIsClustered();

            builder.Property(p => p.Id)
                .ForSqlServerUseSequenceHiLo("SEQ_Categories");

            builder.Property(p => p.Name)
                .HasColumnType("varchar(128)");

            builder.Ignore(p => p.Notifications);
        }
    }
}
