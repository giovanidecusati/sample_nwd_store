using Nwd.BackOffice.SalesContext.Commands.Outputs;
using Nwd.BackOffice.SalesContext.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nwd.BackOffice.SalesContext.Repositories
{
    public interface IProductRepository
    {
        Task CreateAsync(Product entity);

        void Update(Product entity);

        void Delete(Product entity);

        Task<Product> GetProductByIdAsync(Guid ProductId);

        Task<IEnumerable<GetProductListResult>> GetProducListAsync(int page, int pageSize, string orderBy, bool ascending);
    }
}
