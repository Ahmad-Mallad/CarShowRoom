using CarShowRoom.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarShowRoom.ViewComponents
{
    public class FeatureViewComponent:ViewComponent
    {
        private AppDbContext db;

        public FeatureViewComponent(AppDbContext _db)
        {
            db = _db;
        }




        public IViewComponentResult Invoke()
        {
            var ActiveFeatures = db.Features.Where(x => x.IsActive && !x.IsDeleted).ToList();
            return View(ActiveFeatures);
        }
    }
}
