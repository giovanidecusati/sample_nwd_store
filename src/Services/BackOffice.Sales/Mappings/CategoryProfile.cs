using AutoMapper;

namespace BackOffice.Sales.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Models.CategoryViewModel, Data.Entities.Category>();
        }
    }
}
