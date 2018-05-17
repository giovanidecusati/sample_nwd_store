using System.Threading.Tasks;
using BuildingBlock.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Nwd.Commerce.CatalogsContext.Entities;
using Nwd.Commerce.CatalogsContext.Repositories;
using Nwd.IntegrationEvents.Sales;

namespace Nwd.Commerce.CatalogsContext.IntegrationEventHandlers
{
    public class ProductIntegrationEventHandler : IIntegrationEventHandler<ProductApprovedIntegrationEvent>
    {
        private readonly ILogger<ProductIntegrationEventHandler> _logger;
        private readonly IProductRepository _productRepository;

        public ProductIntegrationEventHandler(ILogger<ProductIntegrationEventHandler> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task Handle(ProductApprovedIntegrationEvent @event)
        {
            _logger.LogInformation("Reveived message ProductApprovedIntegrationEvent: {0}", JsonConvert.SerializeObject(@event));
            await _productRepository.Save(
                new Product()
                {
                    CategoryId = @event.CategoryId,
                    CategoryName = @event.CategoryName,
                    Price = @event.Price,
                    ProductId = @event.ProductId,
                    ProductName = @event.ProductName
                });
        }
    }
}
