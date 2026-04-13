using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CLDV6211_Assignment_Part_1_St10449059.Data;
namespace CLDV6211_Assignment_Part_1_St10449059.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context) { _context = context; }
        public async Task<IActionResult> Index()
        {
            // Fetch venues for the main model
            var venues = await _context.Venues.ToListAsync();

            // Fetch events and bookings and store them in ViewBag
            ViewBag.Events = await _context.Events.Include(e => e.Venue).ToListAsync();
            ViewBag.Bookings = await _context.Bookings.Include(b => b.Event).ToListAsync();

            return View(venues);
        }
    }
}
