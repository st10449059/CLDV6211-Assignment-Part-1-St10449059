using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLDV6211_Assignment_Part_1_St10449059.Models
{
   
    public class Booking
    {
        // [Key] identifies this property as the unique Primary Key (Microsoft, 2023).
        [Key]
        public int BookingId { get; set; }

        // [Required] maps to a NOT NULL constraint in the database (Freeman, 2022).
        [Required]
        [Display(Name = "Attendee Name")]
        public string AttendeeName { get; set; }

        /* * Foreign Key:
         * EventId links each booking to a specific event record, enforcing the 
         * relational structure defined in the database script.
         */
        public int EventId { get; set; }

        /* * Navigation Property:
         * This allows Entity Framework to 'join' the Event table, providing access 
         * to Event details (like EventName) directly from a Booking object (Lerman & Miller, 2015).
         */
        [ForeignKey("EventId")]
        public virtual Event? Event { get; set; }
    }
}
