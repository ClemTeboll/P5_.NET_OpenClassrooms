using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Areas.Admin.DTO.Read;
using TheCarHub.Data;

namespace TheCarHub.Areas.ProductDetails
{
    [Area("ProductDetails")]
    public class ProductDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public ProductDetailsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: ProductDetails/Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .Where(c => c.Id == id)
                .Include(x => x.CarDetails)
                .Include(x => x.CarDetails.CarMakes)
                .Include(x => x.CarDetails.CarModel)
                .Include(y => y.CarImages)
                .FirstOrDefaultAsync();

            if (car == null)
            {
                return NotFound();
            }

            var carObject = (car, car.CarImages.First(), car.CarDetails);
            CarDtoRead carDtoRead = _mapper.Map<CarDtoRead>(carObject);

            return View(carDtoRead);
        }
    }
}
