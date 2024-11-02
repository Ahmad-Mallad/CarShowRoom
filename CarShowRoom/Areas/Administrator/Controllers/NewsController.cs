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
    public class NewsController : Controller
    {
        private readonly AppDbContext _context;

        public NewsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Administrator/News
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.News.Include(n => n.Brand);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Administrator/News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Brand)
                .FirstOrDefaultAsync(m => m.NewsId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: Administrator/News/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName");
            return View();
        }

        // POST: Administrator/News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsId,NewsImg,NewsTitle,NewsContent,PublishDate,BrandId,IsActive,IsDeleted,CreationDate")] News news, IFormFile ImgFile)
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

                    news.NewsImg = ImgFile.FileName; // Store the image file name in NewsImg
                }

                _context.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", news.BrandId);
            return View(news);
        }

		// GET: Administrator/news/Edit/5
		// GET: Administrator/News/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var news = await _context.News.FindAsync(id);
			if (news == null)
			{
				return NotFound();
			}

			// Populate the dropdown for Brand selection
			ViewData["BrandId"] = new SelectList(_context.Brands.Where(b => b.IsActive && !b.IsDeleted), "BrandId", "BrandName", news.BrandId);

			return View(news);
		}




		// POST: Administrator/News/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NewsId,NewsImg,NewsTitle,NewsContent,PublishDate,BrandId,IsActive,IsDeleted,CreationDate")] News news)
        {
            if (id != news.NewsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.NewsId))
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", news.BrandId);
            return View(news);
        }

        // GET: Administrator/News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Brand)
                .FirstOrDefaultAsync(m => m.NewsId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: Administrator/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.NewsId == id);
        }
    }
}
