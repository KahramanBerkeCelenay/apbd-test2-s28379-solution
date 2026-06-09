using System.ComponentModel.DataAnnotations;

namespace Test2.Entities
{
    public class VenueDetails
    {
        public int VenueId { get; set; }
        public Venue Venue { get; set; } = null!;

        public int ParkingSpaces { get; set; }

        [MaxLength(200)]
        public string AccessibilityInfo { get; set; } = null!;

        [MaxLength(200)]
        public string? WebsiteUrl { get; set; }
    }
}
