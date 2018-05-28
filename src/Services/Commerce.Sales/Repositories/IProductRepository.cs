using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Commerce.Catalogs.Entities;

namespace Commerce.Catalogs.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> FindProductsByFilterAsync(string filter, int page, int pageSize, string orderBy, bool ascending);
        Task<IEnumerable<Product>> GetFeaturedAsync(int page, int pageSize, string orderBy, bool ascending);
        Task<Product> GetProductByIdAsync(int productId);
        Task SaveAsync(Product entity);
    }
}