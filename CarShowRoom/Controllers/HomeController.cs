using CarShowRoom.Data;
using CarShowRoom.Models;
using CarShowRoom.Models.DataModel;
using CarShowRoom.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders.Testing;
using System.Diagnostics;

namespace CarShowRoom.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AppDbContext db;
        

        public HomeController(ILogger<HomeController> logger,AppDbContext _db)
        {
            _logger = logger;
            db = _db;
          
        }

        public IActionResult Index()
        {
            IndexDataModel model = new IndexDataModel
            {
                // Filter brands to include only those that are active and not deleted
                Brands = db.Brands.ToList(),
                News = db.News.Where(b => b.IsActive && !b.IsDeleted).ToList(),
            };
            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        [Authorize]
        public IActionResult BrandDetails(int id)
        {
            // Retrieve the brand and filter car models based on IsActive and IsDeleted
            var brand = db.Brands
                          .Where(b => b.BrandId == id && b.IsActive && !b.IsDeleted)
                          .Select(b => new BrandDetailsModel
                          {
                              BrandName = b.BrandName,
                              BrandImg = b.BrandImg,
                              CarModels = b.CarModels
                                             .Where(c => c.IsActive && !c.IsDeleted) // Filter car models
                                             .Select(c => new CarModelViewModel
                                             {
                                                 CarModelName = c.CarModelName,
                                                 CarImage = c.CarImage,
                                                 Description = c.Description,
                                                 Price = c.Price
                                             }).ToList()
                          })
                          .FirstOrDefault();

            if (brand == null)
            {
                return NotFound(); // Handle case where the brand is not found
            }

            return View(brand); // Pass the filtered model to the view
        }



        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitContactForm(ContactSubmission submission)
        {
            if (ModelState.IsValid)
            {
                db.ContactSubmissions.Add(submission);
                await db.SaveChangesAsync();
                return RedirectToAction("ThankYou"); // Redirect to a thank you page or show a success message
            }
            return View("Contact"); // Return to the contact form if validation fails
        }


        public IActionResult ThankYou()
        {
            return View(); // Create a simple Thank You view to display after form submission
        }









    }
}
