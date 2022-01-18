using AutoMapper;

using Models;
using Services.ViewModels;

namespace Services.MapperSettings
{
    public class Profiles : Profile
    {
       public Profiles()
        {
            CreateMap<Region, RegionViewModel>()
               // .ForPath(x=>x.Countries, z => z.MapFrom(a=>a.Countries))
                .ReverseMap();

            CreateMap<Country, CountryViewModel>()
               // .ForPath(x=>x.Orders, z=>z.MapFrom(a=>a.Orders))
                .ReverseMap();

            CreateMap<Order, OrderViewModel>()
               // .ForPath(x=>x.Sales, z=>z.MapFrom(a=>a.Sales))
                .ReverseMap();

            CreateMap<Sales, SalesViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}