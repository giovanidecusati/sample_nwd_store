using BuildingBlock.Core.IntegrationEvents;

namespace BackOffice.Sales.Contracts.v1
{
    public class ProductCreatedIntegrationEvent : IntegrationEvent
    {
        public ProductCreatedIntegrationEvent(int productId, string productName, int categoryId, string categoryName, decimal price, bool featured)
        {
            ProductId = productId;
            ProductName = productName;
            CategoryId = categoryId;
            CategoryName = categoryName;
            Price = price;
            Featured = featured;
        }

        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int CategoryId { get; private set; }
        public string CategoryName { get; private set; }
        public decimal Price { get; private set; }
        public bool Featured { get; private set; }
    }
}
