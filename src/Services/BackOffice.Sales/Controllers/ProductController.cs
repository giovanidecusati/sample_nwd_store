using AutoMapper;
using BackOffice.Sales.Contracts.v1;
using BackOffice.Sales.Data.Contexts;
using BackOffice.Sales.Data.Entities;
using BackOffice.Sales.Models;
using BackOffice.Sales.Services;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BackOffice.Sales.Controllers
{
    [Route("v1/[controller]")]
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
        public async Task<IActionResult> Post(ProductViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            var product = Mapper.Map<Product>(model);

            var category = await _context.Categories.FirstOrDefaultAsync(p => p.Id == model.CategoryId);

            product.Category = category;

            await _context.Products.AddAsync(product);

            var @event = new ProductCreatedIntegrationEvent(
                product.Id,
                product.Name,
                product.Category.Id,
                product.Category.Name,
                product.Price,
                product.Featured);

            await _integrationEvent.SaveEventAndProductContextChangesAsync(@event);

            await _integrationEvent.PublishThroughEventBusAsync(@event);

            return CreatedAtRoute(new { product.Id }, product);
        }

        [HttpPut]
        public async Task<IActionResult> Put(ProductViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == model.Id);

            if (product == null)
                return NotFound();

            product = Mapper.Map<ProductViewModel, Product>(model);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            _context.Remove(product);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _context.Products.AsNoTracking()
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            return Ok(Mapper.Map<ProductViewModel>(product));
        }
    }
}