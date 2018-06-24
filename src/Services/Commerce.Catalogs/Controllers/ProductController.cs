using BuildingBlock.Core.Paging;
using Commerce.Catalogs.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
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
        public async Task<IActionResult> Get(int productId, CancellationToken cancellationToken)
        {
            var result = await _productRepository.GetProductByIdAsync(productId, cancellationToken);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("featured")]
        public async Task<IActionResult> GetByFeatured(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return Ok(await _productRepository.GetFeaturedAsync(pageNumber, pageSize, cancellationToken));
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetByFilter(PagedRequest pagedRequest, string filter, CancellationToken cancellationToken)
        {
            return Ok(await _productRepository.FindProductsByFilterAsync(pagedRequest, filter, cancellationToken));
        }
    }
}