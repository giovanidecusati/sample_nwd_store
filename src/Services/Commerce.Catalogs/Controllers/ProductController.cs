using Microsoft.AspNetCore.Mvc;
using Commerce.Catalogs.Repositories;
using System;
using System.Threading.Tasks;

namespace Commerce.Catalogs.Controllers
{
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int productId)
        {
            var result = await _productRepository.GetProductByIdAsync(productId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("featured")]
        public async Task<IActionResult> GetFeatured(int page, int pageSize, string orderBy, bool ascending)
        {
            return Ok(await _productRepository.GetFeaturedAsync(page, pageSize, orderBy, ascending));
        }
    }
}