using Commerce.Catalogs.Entities;
using Nest;

namespace Commerce.Catalogs.Repositories
{
    public static class IndexMapperExtension
    {
        public static void MapIndexes(this ConnectionSettings conenctionSettings)
        {
            conenctionSettings.DefaultIndex("northwind_store");
            conenctionSettings.DefaultMappingFor<Product>(m => m.TypeName(nameof(Product)));
        }
    }
}
