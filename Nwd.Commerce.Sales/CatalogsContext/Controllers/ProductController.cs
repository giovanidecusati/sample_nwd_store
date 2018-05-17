using Microsoft.AspNetCore.Mvc;
using Nwd.Commerce.CatalogsContext.Repositories;
using System;
using System.Threading.Tasks;

namespace Nwd.Commerce.CatalogsContext.Controllers
{
    [Route("v1/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Get(Guid productId)
        {
            return Ok(await _productRepository.GetProductById(productId));
        }
    }
}