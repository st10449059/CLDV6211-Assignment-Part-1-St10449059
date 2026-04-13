using System.ComponentModel.DataAnnotations;
namespace CLDV6211_Assignment_Part_1_St10449059.Models
{
    public class Venue
    {
        [Key]
        public int VenueId { get; set; }
        [Required]
        public string VenueName { get; set; }
        [Required]
        public string Location { get; set; }
        public string? ImageUrl { get; set; } // Placeholder URL support
        public virtual ICollection<Event>? Events { get; set; }
    }
}
