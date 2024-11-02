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
    public class MenusController : Controller
    {
        private readonly AppDbContext _context;

        public MenusController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Administrator/Menus
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Menus.Include(m => m.ParentMenu);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Administrator/Menus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .Include(m => m.ParentMenu)
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

		// GET: Administrator/Menus/Create
		public IActionResult Create()
		{
			// Fetch only active menus that are not deleted
			ViewData["ParentId"] = new SelectList(_context.Menus.Where(m => m.IsActive && !m.IsDeleted), "MenuId", "MenuTitle");
			return View();
		}

		// POST: Administrator/Menus/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("MenuId,MenuTitle,MenuUrl,ParentId,IsActive,IsDeleted,CreationDate")] Menu menu)
		{
			if (ModelState.IsValid)
			{
				_context.Add(menu);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["ParentId"] = new SelectList(_context.Menus.Where(m => m.IsActive && !m.IsDeleted), "MenuId", "MenuTitle", menu.ParentId);
			return View(menu);
		}

		// GET: Administrator/Menus/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var menu = await _context.Menus.FindAsync(id);
			if (menu == null)
			{
				return NotFound();
			}
			ViewData["ParentId"] = new SelectList(_context.Menus.Where(m => m.IsActive && !m.IsDeleted), "MenuId", "MenuTitle", menu.ParentId);
			return View(menu);
		}

		// POST: Administrator/Menus/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("MenuId,MenuTitle,MenuUrl,ParentId,IsActive,IsDeleted,CreationDate")] Menu menu)
		{
			if (id != menu.MenuId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(menu);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!MenuExists(menu.MenuId))
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
			ViewData["ParentId"] = new SelectList(_context.Menus.Where(m => m.IsActive && !m.IsDeleted), "MenuId", "MenuTitle", menu.ParentId);
			return View(menu);
		}


		// GET: Administrator/Menus/Delete/5
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .Include(m => m.ParentMenu)
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Administrator/Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
            return _context.Menus.Any(e => e.MenuId == id);
        }
    }
}
