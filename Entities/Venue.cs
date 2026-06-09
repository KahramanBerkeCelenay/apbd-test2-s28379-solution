using System.ComponentModel.DataAnnotations;

namespace Test2.Entities
{
    public class Venue
    {
        public int VenueId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(200)]
        public string Address { get; set; } = null!;

        public int Capacity { get; set; }

        [MaxLength(9)]
        public string Phone { get; set; } = null!;

        public VenueDetails? VenueDetails { get; set; }

        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
