using BackOffice.Sales.Contracts.v1;
using Commerce.Catalogs.Entities;
using Commerce.Catalogs.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Commerce.Catalogs.IntegrationEventHandlers
{
    public class ProductIntegrationEventHandler : IConsumer<ProductCreatedIntegrationEvent>
    {
        private readonly ILogger<ProductIntegrationEventHandler> _logger;
        private readonly IProductRepository _productRepository;

        public ProductIntegrationEventHandler(
            ILogger<ProductIntegrationEventHandler> logger, 
            IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task Consume(ConsumeContext<ProductCreatedIntegrationEvent> context)
        {
            var @event = context.Message;

            _logger.LogInformation($"Received message: {JsonConvert.SerializeObject(@event)}");

            await _productRepository.SaveAsync(
                new Product()
                {
                    CategoryId = @event.CategoryId,
                    CategoryName = @event.CategoryName,
                    Price = @event.Price,
                    Id = @event.ProductId,
                    Name = @event.ProductName,
                    Featured = @event.Featured
                });
        }
    }
}
