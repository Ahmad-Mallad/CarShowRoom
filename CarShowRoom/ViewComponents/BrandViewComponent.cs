using CarShowRoom.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarShowRoom.ViewComponents
{
    public class BrandViewComponent : ViewComponent
    {
        private readonly AppDbContext db;

        public BrandViewComponent(AppDbContext _db)
        {
            db = _db;
        }

        public IViewComponentResult Invoke(string searchString = null)
        {
            var activeBrands = db.Brands
                .Where(b => b.IsActive && !b.IsDeleted);

            // Filter based on the search string if provided
            if (!string.IsNullOrEmpty(searchString))
            {
                activeBrands = activeBrands.Where(b => b.BrandName.ToLower().Contains(searchString.ToLower()));
            }

            return View(activeBrands.ToList()); // Pass the filtered list to the view
        }
    }

}
