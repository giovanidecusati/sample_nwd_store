using AutoMapper;

namespace BackOffice.Sales.Mappings
{
    public static class MapperExtension
    {
        public static void RegisterProfiles()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new ProductProfile());
                cfg.AddProfile(new CategoryProfile());
            });
        }
    }
}
