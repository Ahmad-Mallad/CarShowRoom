using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarShowRoom.Data;
using CarShowRoom.Models;

namespace CarShowRoom.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class CarModelsController : Controller
    {
        private readonly AppDbContext _context;

        public CarModelsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Administrator/CarModels
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.CarModels.Include(c => c.Brand);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Administrator/CarModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModels
                .Include(c => c.Brand)
                .FirstOrDefaultAsync(m => m.CarModelId == id);
            if (carModel == null)
            {
                return NotFound();
            }

            return View(carModel);
        }

        // GET: Administrator/CarModels/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName");
            return View();
        }

        // POST: Administrator/CarModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarModelId,CarModelName,CarImage,Description,Price,VideoUrl,BrandId,IsActive,IsDeleted,CreationDate")] CarModel carModel, IFormFile ImgFile)
        {
            if (ModelState.IsValid)
            {
                // Handle image upload
                if (ImgFile != null && ImgFile.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot/assets/img/", ImgFile.FileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await ImgFile.CopyToAsync(stream);
                    }

                    carModel.CarImage = ImgFile.FileName;  // Store image file name
                }

                // Add video URL (already bound through the model)
                carModel.VideoUrl = carModel.VideoUrl;

                // Save the new CarModel record
                _context.Add(carModel);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", carModel.BrandId);
            return View(carModel);
        }


        // GET: Administrator/CarModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModels.FindAsync(id);
            if (carModel == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", carModel.BrandId);
            return View(carModel);
        }

        // POST: Administrator/CarModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarModelId,CarModelName,CarImage,Description,Price,VideoUrl,BrandId,IsActive,IsDeleted,CreationDate")] CarModel carModel)
        {
            if (id != carModel.CarModelId)
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
                    if (!CarModelExists(carModel.CarModelId))
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", carModel.BrandId);
            return View(carModel);
        }

        // GET: Administrator/CarModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModels
                .Include(c => c.Brand)
                .FirstOrDefaultAsync(m => m.CarModelId == id);
            if (carModel == null)
            {
                return NotFound();
            }

            return View(carModel);
        }

        // POST: Administrator/CarModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carModel = await _context.CarModels.FindAsync(id);
            if (carModel != null)
            {
                _context.CarModels.Remove(carModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarModelExists(int id)
        {
            return _context.CarModels.Any(e => e.CarModelId == id);
        }
    }
}
