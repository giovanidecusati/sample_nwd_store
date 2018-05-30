using AutoMapper;
using BackOffice.Sales.Data.Entities;
using BackOffice.Sales.Features.ProductFeature;

namespace BackOffice.Sales.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductViewModel, Data.Entities.Product>();
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.CategoryId, opts => opts.MapFrom(src => src.Category.Id));
        }
    }
}
