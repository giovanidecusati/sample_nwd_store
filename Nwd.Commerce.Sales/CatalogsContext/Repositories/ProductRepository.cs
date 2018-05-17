using Nest;
using Nwd.Commerce.CatalogsContext.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nwd.Commerce.CatalogsContext.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private static readonly string IndexName = "products";
        private readonly ElasticClient _client;

        public ProductRepository(ElasticClient client)
        {
            _client = client;
        }

        public async Task Save(Product entity)
        {
            await _client.IndexAsync(entity, idx => idx.Index(IndexName));
        }

        public async Task<IEnumerable<Product>> FindProductsByFilter(string filter, int page, int pageSize, string orderBy, bool ascending)
        {
            var result = await _client.SearchAsync<Product>(s =>
                s.From(page * pageSize)
                .Size(pageSize)
                .Query(q =>
                    q.Term(t => t.ProductName, filter) ||
                    q.Term(t => t.CategoryName, filter)));

            return result.Documents;
        }

        public async Task<Product> GetProductById(Guid productId)
        {
            var result = await _client.GetAsync<Product>(productId, idx => idx.Index(IndexName));
            return result.Source;
        }
    }
}
