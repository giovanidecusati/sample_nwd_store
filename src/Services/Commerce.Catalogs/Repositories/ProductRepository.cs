using Nest;
using Commerce.Catalogs.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commerce.Catalogs.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private static readonly string IndexName = "products";
        private readonly ElasticClient _client;

        public ProductRepository(ElasticClient client)
        {
            _client = client;
        }

        public async Task SaveAsync(Product entity)
        {
            await _client.IndexAsync(entity, idx => idx.Index(IndexName));
        }

        public async Task<IEnumerable<Product>> FindProductsByFilterAsync(string filter, int page, int pageSize, string orderBy, bool ascending)
        {
            var result = await _client.SearchAsync<Product>(s =>
                            s.Index(IndexName)
                            .From(page * pageSize)
                            .Size(pageSize)
                            .Query(q =>
                                q.Term(t => t.Name, filter) ||
                                q.Term(t => t.CategoryName, filter)));

            return result.Documents;
        }

        public async Task<IEnumerable<Product>> GetFeaturedAsync(int page, int pageSize, string orderBy, bool ascending)
        {
            var result = await _client.SearchAsync<Product>(s =>
                            s.Index(IndexName)
                            .From(page * pageSize)
                            .Size(pageSize)
                            .Sort(sort => sort.Ascending(p => p.Name))
                            .Query(q => q.Term(t => t.Featured, true)));

            return result.Documents;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var result = await _client.GetAsync<Product>(productId, idx => idx.Index(IndexName));
            return result.Source;
        }
    }
}
