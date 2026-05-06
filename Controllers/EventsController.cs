using CLDV6211_Assignment_Part_1_St10449059.Data;
using CLDV6211_Assignment_Part_1_St10449059.Models;
using CLDV6211_Assignment_Part_1_St10449059.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

/* 
 * CODE ATTRIBUTION & REFERENCE:
 * The logic for double-booking validation and eager loading was developed using:
 * Microsoft. (2023). Handle concurrency exceptions in Entity Framework Core. 
 * Available at: https://learn.microsoft.com/en-us/ef/core/saving/concurrency
 */
namespace CLDV6211_Assignment_Part_1_St10449059.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly BlobService _blobService;

        public EventsController(ApplicationDbContext context, BlobService blobService)
        {
            _context = context;
            _blobService = blobService;
        }

        public async Task<IActionResult> Index()
        {
            // Requirement: Consolidate information from Venue and Event tables
            return View(await _context.Events.Include(e => e.Venue).ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,EventName,EventDate,VenueId")] Event @event, IFormFile imageFile)
        {
            // Requirement: Prevent double booking of a venue for the same date/time
            var isAlreadyBooked = await _context.Events.AnyAsync(e => e.VenueId == @event.VenueId && e.EventDate == @event.EventDate);

            if (isAlreadyBooked)
            {
                // Requirement: Display an alert to the user
                ModelState.AddModelError("", "Validation Error: This venue is already booked for the selected date and time.");
                ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", @event.VenueId);
                return View(@event);
            }

            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    // Integration with Azurite Blob Storage for Part 2
                    @event.ImageUrl = await _blobService.UploadFileAsync(imageFile, "event-images");
                }
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", @event.VenueId);
            return View(@event);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Requirement: Restrict deletion of events associated with active bookings
            var hasActiveBookings = await _context.Bookings.AnyAsync(b => b.EventId == id);

            if (hasActiveBookings)
            {
                TempData["Error"] = "Validation Error: Cannot delete an event that has active attendee bookings.";
                return RedirectToAction(nameof(Index));
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}