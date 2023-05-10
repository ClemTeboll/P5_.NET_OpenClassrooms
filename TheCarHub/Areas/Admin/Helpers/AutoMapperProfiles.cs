using AutoMapper;
using TheCarHub.Areas.Admin.DTO;
using TheCarHub.Areas.Admin.Models;

namespace TheCarHub.Areas.Admin.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CarDTO, Car>();
        }
    }
}
