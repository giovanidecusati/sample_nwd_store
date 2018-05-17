using System;

namespace Nwd.Commerce.CatalogsContext.Entities
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
    }
}
