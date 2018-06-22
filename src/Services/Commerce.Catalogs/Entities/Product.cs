using System;

namespace Commerce.Catalogs.Entities
{
    public class Product
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Featured { get; set; }
    }
}
