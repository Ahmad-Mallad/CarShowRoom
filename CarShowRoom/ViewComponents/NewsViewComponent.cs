using CarShowRoom.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarShowRoom.ViewComponents
{
    public class NewsViewComponent:ViewComponent
    {
        private AppDbContext db;

        public NewsViewComponent(AppDbContext _db)
        {
            db = _db;
        }




        public IViewComponentResult Invoke()
        {
            var ActiveNews=db.News.Where(x => x.IsActive && !x.IsDeleted).ToList();
            return View(ActiveNews);
        }






    }
}
