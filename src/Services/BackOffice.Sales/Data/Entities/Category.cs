using BuildingBlock.Core.Domain;

namespace BackOffice.Sales.Data.Entities
{
    public class Category : EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}