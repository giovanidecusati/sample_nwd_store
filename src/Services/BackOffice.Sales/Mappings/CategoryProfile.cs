using AutoMapper;
using BackOffice.Sales.Data.Entities;
using BackOffice.Sales.Features.CategoryFeature;

namespace BackOffice.Sales.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryViewModel, Category>();
        }
    }
}
