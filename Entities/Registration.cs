using System.ComponentModel.DataAnnotations;

namespace Test2.Entities
{
    public class Registration
    {
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        public int AttendeeId { get; set; }
        public Attendee Attendee { get; set; } = null!;

        public DateTime RegisteredAt { get; set; }

        [MaxLength(10)]
        public string? SeatNumber { get; set; }
    }
}
