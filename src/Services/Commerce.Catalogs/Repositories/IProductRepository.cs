using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlock.Core.Paging;
using Commerce.Catalogs.Entities;

namespace Commerce.Catalogs.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> FindProductsByFilterAsync(PagedRequest pagedRequest, string filter, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<Product>> GetFeaturedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default(CancellationToken));
        Task<Product> GetProductByIdAsync(int productId, CancellationToken cancellationToken = default(CancellationToken));
        Task SaveAsync(Product entity, CancellationToken cancellationToken = default(CancellationToken));
    }
}