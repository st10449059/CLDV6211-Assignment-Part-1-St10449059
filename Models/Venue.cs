using System.ComponentModel.DataAnnotations;

namespace CLDV6211_Assignment_Part_1_St10449059.Models
{
    public class Venue
    {
        // [Key] identifies VenueId as the Primary Key for the relational schema (Microsoft, 2023).
        [Key]
        public int VenueId { get; set; }

        // [Required] ensures that VenueName and Location are mandatory fields in the database.
        [Required]
        [Display(Name = "Venue Name")]
        public string VenueName { get; set; }

        [Required]
        public string Location { get; set; }

        // Nullable string to support optional image URL paths for visual representation.
        public string? ImageUrl { get; set; }

        /* * Navigation Property:
         * Represents a 1:Many relationship. A single Venue can be associated 
         * with multiple Events. The 'virtual' keyword enables Lazy Loading 
         * performance optimizations (Lerman & Miller, 2015).
         */
        public virtual ICollection<Event>? Events { get; set; }
    }
}