using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CLDV6211_Assignment_Part_1_St10449059.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        [Required]
        public string AttendeeName { get; set; }
        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public virtual Event? Event { get; set; }
    }
}

