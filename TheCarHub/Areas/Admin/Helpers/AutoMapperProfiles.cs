using AutoMapper;
using System.Globalization;
using TheCarHub.Areas.Admin.DTO;
using TheCarHub.Areas.Admin.DTO.Read;
using TheCarHub.Areas.Admin.Models;

namespace TheCarHub.Areas.Admin.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CarDTO, Car>();

            CreateMap<CarDTO, CarDetails>()
                .ForMember(
                    dest => dest.Year,
                    opt => opt.MapFrom(
                        src => DateTime.ParseExact("01/01/" + src.Year, "M/d/yyyy", CultureInfo.InvariantCulture)
                    )
                 );


            CreateMap<(CarImage, CarDetails), CarDtoRead>()
                .ForMember(dest => dest.UrlImage, opt => opt.MapFrom(src => src.Item1.UrlImage))
                .ForMember(dest => dest.VIN, opt => opt.MapFrom(src => src.Item2.VIN))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Item2.Year))
                .ForMember(dest => dest.Make, opt => opt.MapFrom(src => src.Item2.Make))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Item2.Model))
                .ForMember(dest => dest.Trim, opt => opt.MapFrom(src => src.Item2.Trim))
                .ForMember(dest => dest.LotDate, opt => opt.MapFrom(src => src.Item2.LotDate))
                .ForMember(dest => dest.SellingPrice, opt => opt.MapFrom(src => src.Item2.SellingPrice));

        }
    }
}
