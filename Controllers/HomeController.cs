using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CLDV6211_Assignment_Part_1_St10449059.Data;

namespace CLDV6211_Assignment_Part_1_St10449059.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor injection provides access to the application database context (Lerman & Miller, 2015).
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Home/Index
        // This action method implements asynchronous data fetching to ensure high responsiveness.
        public async Task<IActionResult> Index()
        {
            // Primary Data: Fetch all venues to serve as the main View Model (Freeman, 2022).
            var venues = await _context.Venues.ToListAsync();

            /* * Secondary Data: Utilizing ViewBag for cross-entity data transfer. 
             * Eager Loading (.Include) is used here to join related tables at the database level, 
             * preventing N+1 query performance issues (Lerman & Miller, 2015).
             */

            // Fetches events while joining the related Venue data.
            ViewBag.Events = await _context.Events.Include(e => e.Venue).ToListAsync();

            // Fetches bookings while joining the related Event data.
            ViewBag.Bookings = await _context.Bookings.Include(b => b.Event).ToListAsync();

            // Returns the main venues list as the strongly-typed model for the Index View.
            return View(venues);
        }
    }
}
