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
            CreateMap<CarDtoWriteCreate, Car>();

            CreateMap<CarDtoWriteEdit, Car>();

            CreateMap<CarDtoWriteEdit, CarDetails>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CarMakesId, opt => opt.MapFrom(src => src.CarMakesId))
                .ForMember(dest => dest.CarModelId, opt => opt.MapFrom(src => src.CarModelId))
                .ForMember(
                    dest => dest.SellingPrice,
                    opt => opt.MapFrom(src => src.Purchase + src.RepairsCost + 500)
                );

            CreateMap<(CarDtoWriteCreate, CarMakes, CarModel), CarDetails>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.VIN, opt => opt.MapFrom(src => src.Item1.VIN))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Item1.Year))
                .ForMember(dest => dest.Trim, opt => opt.MapFrom(src => src.Item1.Trim))
                .ForMember(dest => dest.Purchase, opt => opt.MapFrom(src => src.Item1.Purchase))
                .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => src.Item1.PurchaseDate))
                .ForMember(dest => dest.Repairs, opt => opt.MapFrom(src => src.Item1.Repairs))
                .ForMember(dest => dest.RepairsCost, opt => opt.MapFrom(src => src.Item1.RepairsCost))
                .ForMember(dest => dest.LotDate, opt => opt.MapFrom(src => src.Item1.LotDate.Date))
                .ForMember(
                    dest => dest.SellingPrice,
                    opt => opt.MapFrom(src => src.Item1.Purchase + src.Item1.RepairsCost + 500)
                )
                .ForMember(dest => dest.CarMakesId, opt => opt.MapFrom(src => src.Item2.Id))
                .ForMember(dest => dest.CarMakes, opt => opt.MapFrom(src => src.Item2))
                .ForMember(dest => dest.CarModelId, opt => opt.MapFrom(src => src.Item3.Id))
                .ForMember(dest => dest.CarModel, opt => opt.MapFrom(src => src.Item3));

            CreateMap<(Car, CarImage, CarDetails), CarDtoRead>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Item1.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Item1.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Item1.Description))
                .ForMember(dest => dest.UrlImage, opt => opt.MapFrom(src => src.Item2.UrlImage))
                .ForMember(dest => dest.VIN, opt => opt.MapFrom(src => src.Item3.VIN))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Item3.Year))
                .ForMember(dest => dest.Make, opt => opt.MapFrom(src => src.Item3.CarMakes.Name))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Item3.CarModel.Name))
                .ForMember(dest => dest.Trim, opt => opt.MapFrom(src => src.Item3.Trim))
                .ForMember(dest => dest.LotDate, opt => opt.MapFrom(src => src.Item3.LotDate))
                .ForMember(dest => dest.SellingPrice, opt => opt.MapFrom(src => src.Item3.SellingPrice));

            CreateMap<(Car, CarImage, CarDetails), CarDtoWriteCreate>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Item1.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Item1.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Item1.Description))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Item2.UrlImage))
                .ForMember(dest => dest.VIN, opt => opt.MapFrom(src => src.Item3.VIN))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Item3.Year))
                .ForMember(dest => dest.CarMakesId, opt => opt.MapFrom(src => src.Item3.CarMakes.Name))
                .ForMember(dest => dest.CarModelId, opt => opt.MapFrom(src => src.Item3.CarModel.Name))
                .ForMember(dest => dest.Trim, opt => opt.MapFrom(src => src.Item3.Trim))
                .ForMember(dest => dest.LotDate, opt => opt.MapFrom(src => src.Item3.LotDate))
                .ForMember(dest => dest.SellingPrice, opt => opt.MapFrom(src => src.Item3.SellingPrice));

            CreateMap<(Car, CarImage, CarDetails), CarDtoWriteEdit>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Item1.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Item1.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Item1.Description))
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.Item1.IsAvailable))
                .ForMember(dest => dest.UrlImage, opt => opt.MapFrom(src => src.Item2.UrlImage))
                .ForMember(dest => dest.VIN, opt => opt.MapFrom(src => src.Item3.VIN))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Item3.Year))
                .ForMember(dest => dest.Trim, opt => opt.MapFrom(src => src.Item3.Trim))
                .ForMember(dest => dest.Purchase, opt => opt.MapFrom(src => src.Item3.Purchase))
                .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => src.Item3.PurchaseDate))
                .ForMember(dest => dest.Repairs, opt => opt.MapFrom(src => src.Item3.Repairs))
                .ForMember(dest => dest.RepairsCost, opt => opt.MapFrom(src => src.Item3.RepairsCost))
                .ForMember(dest => dest.LotDate, opt => opt.MapFrom(src => src.Item3.LotDate))
                .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.Item3.SaleDate))
                .ForMember(dest => dest.SellingPrice, opt => opt.MapFrom(src => src.Item3.SellingPrice))
                .ForMember(dest => dest.CarMakesId, opt => opt.MapFrom(src => src.Item3.CarMakes.Id))
                .ForMember(dest => dest.CarModelId, opt => opt.MapFrom(src => src.Item3.CarModel.Id));
        }
    }
}
