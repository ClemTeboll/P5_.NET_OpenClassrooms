﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Areas.Admin.Models;
using TheCarHub.Data;

namespace TheCarHub.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/CarModels
        public async Task<IActionResult> Index()
        {
              return _context.CarModel != null ? 
                          View(await _context.CarModel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CarModel'  is null.");
        }

        // GET: Admin/CarModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarModel == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carModel == null)
            {
                return NotFound();
            }

            return View(carModel);
        }

        // GET: Admin/CarModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CarModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CarModel carModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carModel);
        }

        // GET: Admin/CarModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CarModel == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModel.FindAsync(id);
            if (carModel == null)
            {
                return NotFound();
            }
            return View(carModel);
        }

        // POST: Admin/CarModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CarModel carModel)
        {
            if (id != carModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarModelExists(carModel.Id))
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
            return View(carModel);
        }

        // GET: Admin/CarModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CarModel == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carModel == null)
            {
                return NotFound();
            }

            return View(carModel);
        }

        // POST: Admin/CarModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CarModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CarModel'  is null.");
            }
            var carModel = await _context.CarModel.FindAsync(id);
            if (carModel != null)
            {
                _context.CarModel.Remove(carModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarModelExists(int id)
        {
          return (_context.CarModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
