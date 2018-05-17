using Dapper;
using Microsoft.EntityFrameworkCore;
using Nwd.BackOffice.SalesContext.Commands.Outputs;
using Nwd.BackOffice.SalesContext.Data.Contexts;
using Nwd.BackOffice.SalesContext.Entities;
using Nwd.BackOffice.SalesContext.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nwd.BackOffice.SalesContext.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SalesDbContext _context;

        public ProductRepository(SalesDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Product entity)
        {
            await _context.Products.AddAsync(entity);
        }

        public void Update(Product entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        }

        public void Delete(Product entity)
        {
            _context.Products.Remove(entity);
        }

        public async Task<IEnumerable<GetProductListResult>> GetProducListAsync(int page, int pageSize, string orderBy, bool ascending)
        {
            using (var con = _context.Database.GetDbConnection())
            {
                await con.OpenAsync();

                //var sql = FlepperQueryBuilder
                //    .Select<GetProductListResult>(
                //        As("P.Id", "ProductId"),
                //        As("P.Name", "ProductName"),
                //        As("P.Price", "Price"),
                //        As("C.Id", "CategoryId"),
                //        As("C.Name", "CategoryName"))
                //    .From("sales","Products").As("P")
                //    .InnerJoin("sales","Categories").As("C")
                //    .On("P", "CategoryId").EqualTo("C", "Id")
                //    .Where("1=1")
                //    .OrderBy(orderBy)
                //    .OffSet(page * pageSize).Fetch(pageSize)
                //    .Build();

                var sql = 
$@"
SELECT 
	P.Id AS ProductId
	, P.Name AS ProductName
	, P.Price AS Price
	, C.Id AS CategoryId
	, C.Name AS CategoryName 
FROM 
	[sales].[Products] AS P 
		INNER JOIN [sales].[Categories] AS C 
			ON [P].[CategoryId] = [C].[Id] 
	ORDER BY [ProductId] OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY";

                return await con.QueryAsync<GetProductListResult>(sql);
            }
        }
    }
}
