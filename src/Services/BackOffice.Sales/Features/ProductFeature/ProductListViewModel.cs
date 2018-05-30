using System.ComponentModel.DataAnnotations;

namespace BackOffice.Sales.Features.ProductFeature
{
    public class ProductListViewModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public bool ProductFeatured { get; set; }
        public string CategoryName { get; set; }
    }
}