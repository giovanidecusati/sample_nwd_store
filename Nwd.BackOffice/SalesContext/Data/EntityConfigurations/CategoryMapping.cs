using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nwd.BackOffice.SalesContext.Entities;

namespace Nwd.BackOffice.SalesContext.Data.EntityConfigurations
{
    internal class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories", "sales");            

            builder.Ignore(p => p.Notifications);
        }
    }
}
