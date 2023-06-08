using AutoMapper;
using System.Globalization;
using TheCarHub.Areas.Admin.DTO.Write;
using TheCarHub.Areas.Admin.DTO.Read;
using TheCarHub.Areas.Admin.Models;

namespace TheCarHub.Areas.Admin.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CarDtoWrite, Car>();

            CreateMap<CarDtoWrite, CarDetails>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(
                    dest => dest.Year,
                    opt => opt.MapFrom(
                        src => DateTime.ParseExact("01/01/" + src.Year, "M/d/yyyy", CultureInfo.InvariantCulture)
                    )
                 )
                .ForMember(
                    dest => dest.SellingPrice,
                    opt => opt.MapFrom(src => src.Purchase + src.RepairsCost + 500)
                );

            CreateMap<(Car, CarImage, CarDetails), CarDtoRead>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Item1.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Item1.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Item1.Description))
                .ForMember(dest => dest.UrlImage, opt => opt.MapFrom(src => src.Item2.UrlImage))
                .ForMember(dest => dest.VIN, opt => opt.MapFrom(src => src.Item3.VIN))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Item3.Year))
                //.ForMember(dest => dest.Make, opt => opt.MapFrom(src => src.Item3.Make))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Item3.CarModel.Name))
                .ForMember(dest => dest.Trim, opt => opt.MapFrom(src => src.Item3.Trim))
                .ForMember(dest => dest.LotDate, opt => opt.MapFrom(src => src.Item3.LotDate))
                .ForMember(dest => dest.SellingPrice, opt => opt.MapFrom(src => src.Item3.SellingPrice));

            CreateMap<(Car, CarImage, CarDetails), CarDtoWrite>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Item1.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Item1.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Item1.Description))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Item2.UrlImage))
                .ForMember(dest => dest.VIN, opt => opt.MapFrom(src => src.Item3.VIN))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Item3.Year))
                //.ForMember(dest => dest.Make, opt => opt.MapFrom(src => src.Item3.Make))
                .ForMember(dest => dest.CarModelId, opt => opt.MapFrom(src => src.Item3.CarModel.Name))
                .ForMember(dest => dest.Trim, opt => opt.MapFrom(src => src.Item3.Trim))
                .ForMember(dest => dest.LotDate, opt => opt.MapFrom(src => src.Item3.LotDate))
                .ForMember(dest => dest.SellingPrice, opt => opt.MapFrom(src => src.Item3.SellingPrice));

        }
    }
}
