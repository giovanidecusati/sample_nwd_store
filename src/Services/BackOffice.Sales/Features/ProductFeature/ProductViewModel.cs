using System.ComponentModel.DataAnnotations;

namespace BackOffice.Sales.Features.ProductFeature
{
    public class ProductViewModel 
    {
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(128)]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public bool Featured { get; set; }
    }
}