using CLDV6211_Assignment_Part_1_St10449059.Data;
using CLDV6211_Assignment_Part_1_St10449059.Models;
using CLDV6211_Assignment_Part_1_St10449059.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

/* * CODE ATTRIBUTION & REFERENCE:
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
            return View(await _context.Events.Include(e => e.Venue).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var @event = await _context.Events
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(m => m.EventId == id);

            if (@event == null) return NotFound();

            return View(@event);
        }

        public IActionResult Create()
        {
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,EventName,EventDate,VenueId")] Event @event, IFormFile imageFile)
        {
            // FIX: Use .Date to ensure the entire day is blocked for that venue
            var isAlreadyBooked = await _context.Events.AnyAsync(e =>
                e.VenueId == @event.VenueId &&
                e.EventDate.Date == @event.EventDate.Date);

            if (isAlreadyBooked)
            {
                // This adds the error to the validation summary in the View
                ModelState.AddModelError("", "Validation Error: This venue is already occupied on the selected date.");
                ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", @event.VenueId);
                return View(@event);
            }

            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    @event.ImageUrl = await _blobService.UploadFileAsync(imageFile, "event-images");
                }
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", @event.VenueId);
            return View(@event);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var @event = await _context.Events.FindAsync(id);
            if (@event == null) return NotFound();

            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", @event.VenueId);
            return View(@event);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,EventName,EventDate,VenueId,ImageUrl")] Event @event, IFormFile? imageFile)
        {
            if (id != @event.EventId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (imageFile != null)
                    {
                        @event.ImageUrl = await _blobService.UploadFileAsync(imageFile, "event-images");
                    }
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", @event.VenueId);
            return View(@event);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var @event = await _context.Events
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(m => m.EventId == id);

            if (@event == null) return NotFound();

            return View(@event);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
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

        private bool EventExists(int id) => _context.Events.Any(e => e.EventId == id);
    }
}