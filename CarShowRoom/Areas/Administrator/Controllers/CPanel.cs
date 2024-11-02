using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarShowRoom.Data; // Ensure this using directive is present for AppDbContext

namespace CarShowRoom.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Admin")]
    public class CPanel : Controller
    {
        private readonly AppDbContext _context; // Declare the database context

        public CPanel(AppDbContext context) // Inject the context through the constructor
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Get the count of unread submissions for the badge
            var newSubmissionCount = await _context.ContactSubmissions
                                                   .CountAsync(s => !s.IsRead);

            // Pass the count to the ViewBag
            ViewBag.NewSubmissionCount = newSubmissionCount;

            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ViewSubmissions()
        {
            // Mark unread submissions as read
            var unreadSubmissions = await _context.ContactSubmissions
                                                   .Where(s => !s.IsRead)
                                                   .ToListAsync();

            // Update the IsRead property
            foreach (var submission in unreadSubmissions)
            {
                submission.IsRead = true; // Mark as read
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Retrieve all submissions to display
            var submissions = await _context.ContactSubmissions.ToListAsync();

            // Get the count of new submissions
            var newSubmissionCount = await _context.ContactSubmissions
                                                   .CountAsync(s => !s.IsRead);

            // Pass the count to the view
            ViewBag.NewSubmissionCount = newSubmissionCount;

            return View(submissions);
        }


    }
}
