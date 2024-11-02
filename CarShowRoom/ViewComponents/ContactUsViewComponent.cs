using CarShowRoom.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarShowRoom.ViewComponents
{
    public class ContactUsViewComponent:ViewComponent
    {
        private AppDbContext db;

        public ContactUsViewComponent(AppDbContext _db)
        {
            db = _db;
        }




        public IViewComponentResult Invoke()
        {
            var ActiveContactUs = db.ContactUs.Where(x => x.IsActive && !x.IsDeleted).ToList();
            return View(ActiveContactUs);
        }

    }
}
