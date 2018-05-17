using System;
using BuildingBlock.EventBus.Events;

namespace Nwd.IntegrationEvents.Sales
{
    public class ProductApprovedIntegrationEvent: IntegrationEvent
    {
        public ProductApprovedIntegrationEvent(Guid productId, string productName, Guid categoryId, string categoryName, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            CategoryId = categoryId;
            CategoryName = categoryName;
            Price = price;
        }

        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public Guid CategoryId { get; private set; }
        public string CategoryName { get; private set; }
        public decimal Price { get; private set; }
    }
}
