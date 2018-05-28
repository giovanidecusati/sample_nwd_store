using AutoMapper;

namespace BackOffice.Sales.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Models.ProductViewModel, Data.Entities.Product>();
            CreateMap<Data.Entities.Product, Models.ProductViewModel>()
                .ForMember(dest => dest.CategoryId, opts => opts.MapFrom(src => src.Category.Id));
        }
    }
}
