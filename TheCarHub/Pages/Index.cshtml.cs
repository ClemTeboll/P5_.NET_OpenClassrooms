using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Areas.Admin.DTO.Read;
using TheCarHub.Areas.Admin.Models;
using TheCarHub.Data;

namespace TheCarHub.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<CarDtoRead> _carDtoReadList { get; set; }

        private readonly ILogger<CarDtoRead> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public IndexModel(IWebHostEnvironment webHostEnvironment, ILogger<CarDtoRead> logger, ApplicationDbContext context, IMapper mapper)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public void OnGet()
        {
            List<CarDtoRead> dtoList = new List<CarDtoRead>();

            foreach (Car car in _context.Car
                .Where(x => x.IsAvailable)
                .Include(y => y.CarImages)
                .Include(z => z.CarDetails)
                .Include(z => z.CarDetails.CarModel)
                .AsNoTracking()
                .ToList())
            {
                if (car.IsAvailable)
                {
                    var carObject = (car, car.CarImages.First(), car.CarDetails);
                    CarDtoRead carDtoRead = _mapper.Map<CarDtoRead>(carObject);

                    dtoList.Add(carDtoRead);
                }
                else
                {
                    break;
                }
            }

            _carDtoReadList = dtoList;
        }
    }
}