using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nwd.Commerce.CatalogsContext.Entities;

namespace Nwd.Commerce.CatalogsContext.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> FindProductsByFilter(string filter, int page, int pageSize, string orderBy, bool ascending);
        Task<Product> GetProductById(Guid productId);
        Task Save(Product entity);
    }
}