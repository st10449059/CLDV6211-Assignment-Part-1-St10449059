using Microsoft.EntityFrameworkCore;
using CLDV6211_Assignment_Part_1_St10449059.Models;
namespace CLDV6211_Assignment_Part_1_St10449059.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // These lines are the 'gates' that allow data to flow to the Front End
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}