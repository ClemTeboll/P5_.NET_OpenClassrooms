using AutoMapper;
using System.Globalization;
using TheCarHub.Areas.Admin.DTO;
using TheCarHub.Areas.Admin.Models;

namespace TheCarHub.Areas.Admin.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CarDTO, Car>()
                .ForMember(
                    dest => dest.YearDate, 
                    opt => opt.MapFrom(
                        src => DateTime.ParseExact("01/01/" + src.YearDate, "M/d/yyyy", CultureInfo.InvariantCulture)
                    )
                 );
            CreateMap<CarDTO, CarDetails>();
        }
    }
}
