using System.ComponentModel.DataAnnotations;

namespace Test2.Entities
{
    public class Event
    {
        public int EventId { get; set; }

        public int VenueId { get; set; }
        public Venue Venue { get; set; } = null!;

        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public DateTime EventDate { get; set; }

        public decimal TicketPrice { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = null!;

        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}
