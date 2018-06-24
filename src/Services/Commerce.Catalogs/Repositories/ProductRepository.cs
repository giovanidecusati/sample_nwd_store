using Nest;
using Commerce.Catalogs.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using BuildingBlock.Core.Paging;
using System.Threading;

namespace Commerce.Catalogs.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ElasticClient _client;

        public ProductRepository(ElasticClient client)
        {
            _client = client;
        }

        public async Task SaveAsync(Product entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _client.IndexDocumentAsync(entity, cancellationToken);
        }

        public async Task<IEnumerable<Product>> FindProductsByFilterAsync(PagedRequest pagedRequest, string filter, CancellationToken cancellationToken = default(CancellationToken))
        {
            var sortDescriptor = new SortDescriptor<Product>();
            if (!string.IsNullOrEmpty(pagedRequest.OrderBy))
                sortDescriptor.Field(pagedRequest.OrderBy, pagedRequest.Ascending ? SortOrder.Ascending : SortOrder.Descending);

            // TODO : make this case insensitive search
            var result = await _client.SearchAsync<Product>(s =>
                s.Query(q =>
                    q.Wildcard(t => t.Name.Suffix("keyword"), $"*{filter}*") ||
                    q.Wildcard(t => t.CategoryName.Suffix("keyword"), $"*{filter}*"))
                .From(pagedRequest.PageNumber * pagedRequest.PageSize)
                .Size(pagedRequest.PageSize)
                .Sort(sort => sortDescriptor),
                cancellationToken);

            return result.Documents;
        }

        public async Task<IEnumerable<Product>> GetFeaturedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await _client.SearchAsync<Product>(s =>
                s.Query(q => q.Term(t => t.Featured, true))
                .From(pageNumber * pageSize)
                .Size(pageSize),
                cancellationToken);

            return result.Documents;
        }

        public async Task<Product> GetProductByIdAsync(int productId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await _client.GetAsync<Product>(productId, cancellationToken: cancellationToken);
            return result.Source;
        }
    }
}
