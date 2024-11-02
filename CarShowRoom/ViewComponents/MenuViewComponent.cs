using CarShowRoom.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarShowRoom.ViewComponents
{
    public class MenuViewComponent:ViewComponent
    {


        private  AppDbContext db;

        public MenuViewComponent(AppDbContext _db)
        {
            db = _db;
        }




        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Fetch the top-level menus and include their submenus
            var menus = await db.Menus
                                      .Where(m => m.ParentId == null &&m.IsActive && !m.IsDeleted)
                                      .Include(m => m.SubMenus)
                                      .ToListAsync();

           

            // Define the order of items manually
            var orderedMenuTitles = new List<string> { "Home", "Brands", "Blog","more" };

            var orderedMenus = orderedMenuTitles
                .Select(title => menus.FirstOrDefault(m => m.MenuTitle == title))
                .Where(menu => menu != null)
                .ToList();

            // Add any remaining menus not in the defined order
            orderedMenus.AddRange(menus.Where(m => !orderedMenuTitles.Contains(m.MenuTitle)));

            return View(orderedMenus);
        }






    }

    

}
