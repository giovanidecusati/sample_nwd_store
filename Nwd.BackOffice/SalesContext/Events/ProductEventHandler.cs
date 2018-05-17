using BuildingBlock.Core.DomainEvents;
using BuildingBlock.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Nwd.BackOffice.SalesContext.Repositories;
using Nwd.IntegrationEvents.Sales;
using System.Threading.Tasks;

namespace Nwd.BackOffice.SalesContext.Events
{
    public class ProductEventHandler : IDomainEventHandler<ProductApprovedEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IEventBus _bus;
        private readonly ILogger<ProductEventHandler> _logger;

        public ProductEventHandler(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IEventBus bus, ILogger<ProductEventHandler> logger)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _bus = bus;
            _logger = logger;
        }

        public async Task Handle(ProductApprovedEvent args)
        {
            _logger.LogInformation($"Product {args.ProductId} approved.");

            var product = await _productRepository.GetProductByIdAsync(args.ProductId);
            var category = await _categoryRepository.GetCategoryByIdAsync(product.CategoryId);

            _bus.Publish(new ProductApprovedIntegrationEvent(product.Id, product.Name, category.Id, category.Name, product.Price));

            _logger.LogInformation($"Product {args.ProductId} sent to queue.");
        }

        public void Dispose()
        {
            
        }
    }
}
