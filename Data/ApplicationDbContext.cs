using Microsoft.EntityFrameworkCore;
using CLDV6211_Assignment_Part_1_St10449059.Models;

namespace CLDV6211_Assignment_Part_1_St10449059.Data
{
    
    public class ApplicationDbContext : DbContext
    {
        // The constructor passes the configuration settings (like connection strings) 
        // to the base DbContext class (Microsoft, 2023).
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        /* * DbSet properties represent the tables in the database. 
         * These serve as the 'gates' or gateways that allow the application 
         * to perform LINQ queries against the SQL data (Freeman, 2022).
         */

        // Maps to the Venues table in SQL
        public DbSet<Venue> Venues { get; set; }

        // Maps to the Events table in SQL
        public DbSet<Event> Events { get; set; }

        // Maps to the Bookings table in SQL
        public DbSet<Booking> Bookings { get; set; }
    }
}