using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Areas.Admin.DTO.Read;
using TheCarHub.Areas.Admin.DTO.Write;
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
            List<CarDtoRead> carDtoReadList = new List<CarDtoRead>();

            foreach (Car item in _context.Car)
            {
                Car car = await _context.Car
                    .Where(x => x.Id == item.Id)
                    .Include(y => y.CarImages)
                    .Include(z => z.CarDetails)
                    .Include(z => z.CarDetails.CarModel)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                var carObject = (car, car.CarImages.First(), car.CarDetails);
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

        // GET: Admin/Cars/Create
        public IActionResult Create()
        {
            GetMakesAndModelList();

            return View();
        }

        // POST: Admin/Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind
                ("Id,Name,Description,IsAvailable,Image,VIN,Year,CarMakesId,CarModelId,Trim,PurchaseDate,Purchase,Repairs,RepairsCost,LotDate,SellingPrice,SaleDate")
            ] CarDtoWriteCreate carDtoWriteCreate
            )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Car car = new Car();

                    if (carDtoWriteCreate.Image != null)
                    {
                        string folder = ConfigureUrlImage(carDtoWriteCreate.Image.FileName);
                        await carDtoWriteCreate.Image.CopyToAsync(new FileStream(folder, FileMode.Create));

                        car = _mapper.Map<Car>(carDtoWriteCreate);
                        _context.Add(car);
                        await _context.SaveChangesAsync();

                        CarImage carImage = new CarImage();
                        carImage.UrlImage = folder;
                        carImage.CarId = car.Id;
                        _context.Add(carImage);
                        await _context.SaveChangesAsync();

                        CarMakes carMake = _context.CarMakes.First(cma => cma.Id == carDtoWriteCreate.CarMakesId);
                        CarModel carModel = _context.CarModel.First(cmo => cmo.Id == carDtoWriteCreate.CarModelId);
                        var carObject = (carDtoWriteCreate, carMake, carModel);

                        CarDetails carDetails = _mapper.Map<CarDetails>(carObject);
                        carDetails.CarId = car.Id;
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
            GetMakesAndModelList();

            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .Where(x => x.Id == id)
                .Include(y => y.CarImages)
                .Include(z => z.CarDetails)
                .Include(z => z.CarDetails.CarModel)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (car == null)
            {
                return NotFound();
            }

            var carObject = (car, car.CarImages.First(), car.CarDetails);

            CarDtoWriteEdit carDtoWriteEdit = _mapper.Map<CarDtoWriteEdit>(carObject);
            carDtoWriteEdit.LotDate.ToString("dd/MM/yyyy");

            return View(carDtoWriteEdit);
        }

        // POST: Admin/Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind
                ("Id,Name,Description,IsAvailable,UrlImage,Image,VIN,Year,CarMakesId,CarModelId,Trim,PurchaseDate,Purchase,Repairs,RepairsCost,LotDate,SellingPrice,SaleDate")
            ] CarDtoWriteEdit carDtoWriteEdit
        )
        {
            Car car = await _context.Car
                .Where(x => x.Id == id)
                .Include(y => y.CarImages)
                .Include(z => z.CarDetails)
                .Include(z => z.CarDetails.CarModel)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (id != carDtoWriteEdit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    car = _mapper.Map<CarDtoWriteEdit, Car>(carDtoWriteEdit, car);
                    _context.Update(car);
                    await _context.SaveChangesAsync();

                    if (carDtoWriteEdit.Image != null)
                    {
                        string folder = ConfigureUrlImage(carDtoWriteEdit.Image.FileName);
                        await carDtoWriteEdit.Image.CopyToAsync(new FileStream(folder, FileMode.Create));
                        car.CarImages.Where(x => x.CarId == id).FirstOrDefault().UrlImage = folder;

                        _context.Update(car);
                        await _context.SaveChangesAsync();
                    }

                    car.CarDetails = _mapper.Map(carDtoWriteEdit, car.CarDetails);
                    car.CarDetails.CarMakes = _context.CarMakes.First(cma => cma.Id == carDtoWriteEdit.CarMakesId);
                    car.CarDetails.CarModel = _context.CarModel.First(cmo => cmo.Id == carDtoWriteEdit.CarModelId);

                    _context.Update(car.CarDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(carDtoWriteEdit.Id))
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
            return View(carDtoWriteEdit);
        }

        // GET: Admin/Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .Where(x => x.Id == id)
                .Include(y => y.CarImages)
                .Include(z => z.CarDetails)
                .Include(z => z.CarDetails.CarMakes)
                .Include(z => z.CarDetails.CarModel)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            var carObject = (car, car.CarImages.First(), car.CarDetails);
            CarDtoRead carDtoRead = _mapper.Map<CarDtoRead>(carObject);

            return View(carDtoRead);
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

        public string ConfigureUrlImage(string FileName)
        {
            string folder = "wwwroot/images/";
            string extension = Path.GetExtension(FileName);
            folder += Guid.NewGuid().ToString() + extension;

            return folder;
        }

        public void GetMakesAndModelList()
        {
            List<CarMakes> carMakesList = _context.CarMakes.ToList();
            List<CarModel> carModelList = _context.CarModel.ToList();

            ViewBag.CarMakesList = carMakesList;
            ViewBag.CarModelList = carModelList;
        }
    }
}
