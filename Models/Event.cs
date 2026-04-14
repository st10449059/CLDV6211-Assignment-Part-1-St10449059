using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLDV6211_Assignment_Part_1_St10449059.Models
{
    
    public class Event
    {
        // [Key] denotes the Primary Key for the database table (Microsoft, 2023).
        [Key]
        public int EventId { get; set; }

        // [Required] ensures the field is NOT NULL in the generated SQL schema.
        [Required]
        [Display(Name = "Event Name")]
        public string EventName { get; set; }

        [Required]
        [Display(Name = "Date and Time")]
        public DateTime EventDate { get; set; }

        // Nullable string to allow events without an image path.
        public string? ImageUrl { get; set; }

        /* * Relational Logic:
         * VenueId acts as the Foreign Key linking this event to a specific Venue.
         */
        public int VenueId { get; set; }

        /* * Navigation Properties:
         * The 'virtual' keyword enables Lazy Loading, allowing related data 
         * to be fetched only when required (Lerman & Miller, 2015).
         */
        [ForeignKey("VenueId")]
        public virtual Venue? Venue { get; set; }

        // Represents a 1:Many relationship where one Event can have multiple Bookings.
        public virtual ICollection<Booking>? Bookings { get; set; }
    }
}

