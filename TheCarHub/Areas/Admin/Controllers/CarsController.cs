using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Areas.Admin.DTO.Write;
using TheCarHub.Areas.Admin.DTO.Read;
using TheCarHub.Areas.Admin.Models;
using TheCarHub.Data;
using Microsoft.VisualBasic;
using static NuGet.Packaging.PackagingConstants;

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
            List<Car> carList = await _context.Car.ToListAsync();
            List<CarDtoRead> carDtoReadList = new List<CarDtoRead>();

            foreach (Car car in carList)
            {
                var carObject = GetDataForCarMapping(car);
                CarDtoRead carDtoRead = _mapper.Map<CarDtoRead>(carObject);

                carDtoReadList.Add(carDtoRead);
            }

            return _context.Car != null ?
                        View(carDtoReadList) :
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

            List<CarImage> carImageList = await _context.CarImages.ToListAsync();
            List<CarDetails> carDetailsList = await _context.CarDetails.ToListAsync();
            List<CarDtoRead> carDtoReadList = new List<CarDtoRead>();

            var carObject = GetDataForCarMapping(car);
            CarDtoRead carDtoRead = _mapper.Map<CarDtoRead>(carObject);

            return View(carDtoRead);
        }

        // GET: Admin/Cars/Create
        public IActionResult Create()
        {
            //List<string> carModelList = new List<string>
            //{
            //    "Miata",
            //    "Liberty",
            //    "Grand Caravan",
            //    "Explorer",
            //    "Civic",
            //    "GTI",
            //    "Edge",
            //};
            return View(
                //carModelList
                );
        }

        // POST: Admin/Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind
                ("Id,Name,Description,IsAvailable,Image,VIN,Year,Make,Model,Trim,PurchaseDate,Purchase,Repairs,RepairsCost,LotDate,SellingPrice,SaleDate")
            ] CarDtoWrite carDtoWrite
            )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Car car = new Car();

                    if (carDtoWrite.Image != null)
                    {
                        string folder = configureUrlImage(carDtoWrite);
                        await carDtoWrite.Image.CopyToAsync(new FileStream(folder, FileMode.Create));

                        car = _mapper.Map<Car>(carDtoWrite);
                        _context.Add(car);
                        await _context.SaveChangesAsync();

                        CarImage carImage = new CarImage();
                        carImage.UrlImage = folder;
                        carImage.CarId = car.Id;
                        _context.Add(carImage);
                        await _context.SaveChangesAsync();

                        CarDetails carDetails = _mapper.Map<CarDetails>(carDtoWrite);
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
            return View();
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

            var carObject = GetDataForCarMapping(car);
            CarDtoWrite carDtoWrite = _mapper.Map<CarDtoWrite>(carObject);

            return View(carDtoWrite);
        }

        // POST: Admin/Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind
                ("Id,Name,Description,IsAvailable,Image,VIN,Year,Make,Model,Trim,PurchaseDate,Purchase,Repairs,RepairsCost,LotDate,SellingPrice,SaleDate")
            ] CarDtoWrite carDtoWrite
        )
        {
            Car car = GetCar(carDtoWrite.Id);
            CarImage carImage = GetCarImage(car);
            CarDetails carDetails = GetCarDetails(car);

            if (id != carDtoWrite.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    car = _mapper.Map<Car>(carDtoWrite);
                    _context.Update(car);

                    if (carDtoWrite.Image != null)
                    {
                        string folder = configureUrlImage(carDtoWrite);
                        await carDtoWrite.Image.CopyToAsync(new FileStream(folder, FileMode.Create));
                        carImage.UrlImage = folder;
                        _context.Update(carImage);
                    }

                    carDetails = _mapper.Map<CarDetails>(carDtoWrite);
                    _context.Update(carDetails);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(carDtoWrite.Id))
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
            return View(carDtoWrite);
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

        public Car GetCar(int id)
        {
            List<Car> carList = _context.Car.ToList();
            return carList.Where(c => c.Id == id).First();
        }

        public CarImage GetCarImage(Car car)
        {
            List<CarImage> carImageList = _context.CarImages.ToList();
            return carImageList.Where(ci => ci.CarId == car.Id).First();
        }

        public CarDetails GetCarDetails(Car car)
        {
            List<CarDetails> carDetailsList = _context.CarDetails.ToList();
            CarDetails carDetails = carDetailsList.Where(ci => ci.CarId == car.Id).First();
            return carDetails;
        }

        public dynamic GetDataForCarMapping(Car car)
        {
            CarImage carImage = GetCarImage(car);
            CarDetails carDetails = GetCarDetails(car);
            var carObject = (car, carImage, carDetails);
            return carObject;
        }

        public string configureUrlImage(CarDtoWrite carDtoWrite)
        {
            string folder = "wwwroot/images/";
            string extension = Path.GetExtension(carDtoWrite.Image.FileName);
            folder += Guid.NewGuid().ToString() + extension;

            return folder;
        }
        
    }
}
