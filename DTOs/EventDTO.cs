namespace Test2.DTOs
{
    public class EventDTO
    {
        public int EventId { get; set; }

        public string Name { get; set; } = null!;

        public DateTime EventDate { get; set; }

        public decimal TicketPrice { get; set; }

        public string Status { get; set; } = null!;

        public VenueDTO Venue { get; set; } = null!;

        public List<RegistrationDTO> Registrations { get; set; } = new List<RegistrationDTO>();
    }

    public class VenueDTO
    {
        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public int Capacity { get; set; }

        public string Phone { get; set; } = null!;

        public VenueDetailsDTO Details { get; set; } = null!;
    }

    public class VenueDetailsDTO
    {
        public int ParkingSpaces { get; set; }

        public string AccessibilityInfo { get; set; } = null!;

        public string? WebsiteUrl { get; set; }
    }

    public class RegistrationDTO
    {
        public DateTime RegisteredAt { get; set; }

        public string? SeatNumber { get; set; }

        public AttendeeDTO Attendee { get; set; } = null!;
    }

    public class AttendeeDTO
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;
    }
}
