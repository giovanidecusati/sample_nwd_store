using AutoMapper;
using BackOffice.Sales.Contracts.v1;
using BackOffice.Sales.Data.Contexts;
using BackOffice.Sales.Data.Entities;
using BackOffice.Sales.Services;
using BuildingBlock.Core.Paging;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackOffice.Sales.Features.ProductFeature
{
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductIntegrationEventService _integrationEvent;
        private readonly SalesDbContext _context;

        public ProductController(
            IProductIntegrationEventService integrationEvent,
            SalesDbContext context,
            IBusControl bus)
        {
            _integrationEvent = integrationEvent;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductViewModel model, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            var product = Mapper.Map<Product>(model);

            var category = await _context.Categories.FirstOrDefaultAsync(p => p.Id == model.CategoryId, cancellationToken);

            product.Category = category;

            await _context.Products.AddAsync(product, cancellationToken);

            var @event = new ProductCreatedIntegrationEvent(
                product.Id,
                product.Name,
                product.Category.Id,
                product.Category.Name,
                product.Price,
                product.Featured);

            await _integrationEvent.SaveEventAndProductContextChangesAsync(@event, cancellationToken);

            await _integrationEvent.PublishThroughEventBusAsync(@event, cancellationToken);

            return CreatedAtRoute(new { product.Id }, product);
        }

        [HttpPut]
        public async Task<IActionResult> Put(ProductViewModel model, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == model.Id, cancellationToken);

            if (product == null)
                return NotFound();

            product = Mapper.Map<ProductViewModel, Product>(model);

            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

            if (product == null)
                return NotFound();

            _context.Remove(product);

            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var product = await _context.Products.AsNoTracking()
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

            if (product == null)
                return NotFound();

            return Ok(Mapper.Map<ProductViewModel>(product));
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> List(PagedRequest pagedRequest, string productNameFilter, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = _context.Products.AsNoTracking()
                .Include(p => p.Category)
                .Where(p => p.Name.Contains(productNameFilter) || string.IsNullOrEmpty(productNameFilter));

            var totalNumberOfRecords = await query.CountAsync(cancellationToken);

            var results = await query.Skip(pagedRequest.PageNumber * pagedRequest.PageSize)
                .Take(pagedRequest.PageSize)
                .Select(p => new ProductListViewModel()
                {
                    CategoryName = p.Category.Name,
                    ProductFeatured = p.Featured,
                    ProductId = p.Id,
                    ProductName = p.Name,
                    ProductPrice = p.Price
                })
                .ToListAsync(cancellationToken);

            var result =
                new PagedResults<ProductListViewModel>(
                    pagedRequest.PageNumber,
                    pagedRequest.PageSize,
                    totalNumberOfRecords,
                    results);

            return Ok(result);
        }
    }
}