using Microsoft.AspNetCore.Mvc;
using Nwd.BackOffice.Base;
using Nwd.BackOffice.SalesContext.Commands.Handlers;
using Nwd.BackOffice.SalesContext.Commands.Inputs;
using Nwd.BackOffice.SalesContext.Data.Contexts;
using Nwd.BackOffice.SalesContext.Repositories;
using System;
using System.Threading.Tasks;

namespace Nwd.BackOffice.SalesContext.Controllers
{
    [Route("v1/[controller]/[action]")]
    public class ProductController : BaseController
    {
        private readonly ProductHandler _handler;

        public ProductController(ProductHandler handler, SalesDbContext context) : base(context)
        {
            _handler = handler;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommand command)
        {
            if (!command.IsValid())
                return await CreateResponse(null, command.Notifications);

            var result = await _handler.Handle(command);

            return await CreateResponse(result, _handler.Notifications);
        }

        [HttpPut]
        public async Task<IActionResult> ApproveProduct(ApproveProductCommand command)
        {
            if (!command.IsValid())
                return await CreateResponse(notifications: command.Notifications);

            await _handler.Handle(command);

            return await CreateResponse(notifications: _handler.Notifications);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateProductCommand command)
        {
            if (!command.IsValid())
                return await CreateResponse(notifications: command.Notifications);

            await _handler.Handle(command);

            return await CreateResponse(notifications: _handler.Notifications);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteProductCommand command)
        {
            if (!command.IsValid())
                return await CreateResponse(notifications: command.Notifications);

            await _handler.Handle(command);

            return await CreateResponse(notifications: _handler.Notifications);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromServices] IProductRepository productRepository, Guid productId)
        {
            var product = await productRepository.GetProductByIdAsync(productId);
            if (product == null)
                return NotFound();

            return await CreateResponse(new { product.Id, product.Name, product.Price });
        }

        [HttpGet]
        public async Task<IActionResult> GetByFilter(GetProductsCommand command)
        {
            if (!command.IsValid())
                return await CreateResponse(notifications: command.Notifications);

            return await CreateResponse(await _handler.Handle(command));
        }
    }
}
