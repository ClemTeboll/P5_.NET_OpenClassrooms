using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Areas.Admin.Models;
using TheCarHub.Data;

namespace TheCarHub.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarMakesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarMakesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/CarMakes
        public async Task<IActionResult> Index()
        {
            return _context.CarMakes != null ?
                        View(await _context.CarMakes.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.CarMakes'  is null.");
        }

        // GET: Admin/CarMakes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarMakes == null)
            {
                return NotFound();
            }

            var carMakes = await _context.CarMakes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carMakes == null)
            {
                return NotFound();
            }

            return View(carMakes);
        }

        // GET: Admin/CarMakes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CarMakes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CarMakes carMakes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carMakes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carMakes);
        }

        // GET: Admin/CarMakes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CarMakes == null)
            {
                return NotFound();
            }

            var carMakes = await _context.CarMakes.FindAsync(id);
            if (carMakes == null)
            {
                return NotFound();
            }
            return View(carMakes);
        }

        // POST: Admin/CarMakes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CarMakes carMakes)
        {
            if (id != carMakes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carMakes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarMakesExists(carMakes.Id))
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
            return View(carMakes);
        }

        // GET: Admin/CarMakes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CarMakes == null)
            {
                return NotFound();
            }

            var carMakes = await _context.CarMakes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carMakes == null)
            {
                return NotFound();
            }

            return View(carMakes);
        }

        // POST: Admin/CarMakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CarMakes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CarMakes'  is null.");
            }
            var carMakes = await _context.CarMakes.FindAsync(id);
            if (carMakes != null)
            {
                _context.CarMakes.Remove(carMakes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarMakesExists(int id)
        {
            return (_context.CarMakes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
