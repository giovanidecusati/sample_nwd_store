using System.ComponentModel.DataAnnotations;

namespace BackOffice.Sales.Features.CategoryFeature
{
    public class CategoryViewModel 
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        [MinLength(3)]
        public string Name { get; set; }

    }
}