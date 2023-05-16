using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Areas.Admin.DTO;
using TheCarHub.Areas.Admin.Models;
using TheCarHub.Data;

namespace TheCarHub.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public CarsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        // GET: Admin/Cars
        public async Task<IActionResult> Index()
        {
            return _context.Car != null ?
                        View(await _context.Car.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Car'  is null.");
        }

        // GET: Admin/Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Admin/Cars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind
                ("Id,Name,Description,YearDate,Price,IsAvailable,Image,VIN,Year,Make,Model,Trim,PurchaseDate,Purchase,Repairs,RepairsCost,LotDate,SellingPrice,SaleDate")
            ] CarDTO carDto
            )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Car car = new Car();
                    string folder = "wwwroot/images/";

                    if (carDto.Image != null)
                    {
                        string extension = Path.GetExtension(carDto.Image.FileName);
                        folder += Guid.NewGuid().ToString() + extension;

                        await carDto.Image.CopyToAsync(new FileStream(folder, FileMode.Create));

                        car = _mapper.Map<Car>(carDto);
                        _context.Add(car);

                        await _context.SaveChangesAsync();

                        CarImage carImage = new CarImage();
                        carImage.UrlImage = folder;
                        carImage.CarId = car.Id;
                        _context.Add(carImage);

                        CarDetails carDetails = _mapper.Map<CarDetails>(carDto);
                        carDetails.CarId = car.Id;
                        carDetails.SellingPrice = carDetails.Purchase + carDetails.RepairsCost + 500;
                        _context.Add(carDetails);

                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    var exe = ex.InnerException.Message;
                }
           
                return RedirectToAction(nameof(Index));
        }
            return View(
                //car
                );
    }

        // GET: Admin/Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Admin/Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UrlImage,Description,YearDate,Price,IsAvailable,Image")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Admin/Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Admin/Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Car == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Car'  is null.");
            }
            var car = await _context.Car.FindAsync(id);
            if (car != null)
            {
                _context.Car.Remove(car);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return (_context.Car?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
