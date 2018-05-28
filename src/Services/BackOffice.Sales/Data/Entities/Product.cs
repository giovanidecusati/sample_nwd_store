using BuildingBlock.Core.Domain;

namespace BackOffice.Sales.Data.Entities
{
    public class Product : EntityBase
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Featured { get; set; }
    }
}
