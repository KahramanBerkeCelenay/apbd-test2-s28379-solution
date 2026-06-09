using Test2.Entities;
using Microsoft.EntityFrameworkCore;

namespace Test2.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Attendee> Attendees { get; set; } = null!;
        public DbSet<Venue> Venues { get; set; } = null!;
        public DbSet<VenueDetails> VenueDetails { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<Registration> Registrations { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Attendee>().HasKey(a => a.AttendeeId);
            modelBuilder.Entity<Venue>().HasKey(v => v.VenueId);
            modelBuilder.Entity<VenueDetails>().HasKey(vd => vd.VenueId);
            modelBuilder.Entity<Event>().HasKey(e => e.EventId);
            modelBuilder.Entity<Registration>()
                .HasKey(r => new { r.EventId, r.AttendeeId });

            modelBuilder.Entity<Event>()
                .Property(e => e.TicketPrice)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<VenueDetails>()
                .HasOne(vd => vd.Venue)
                .WithOne(v => v.VenueDetails)
                .HasForeignKey<VenueDetails>(vd => vd.VenueId);

            modelBuilder.Entity<Venue>().HasData(
                new Venue { VenueId = 1, Name = "National Concert Hall", Address = "Marszalkowska 12, Warsaw", Capacity = 500, Phone = "111222333" },
                new Venue { VenueId = 2, Name = "Open Air Stage", Address = "Lazienki Park 1, Warsaw", Capacity = 1000, Phone = "222333444" },
                new Venue { VenueId = 3, Name = "Gallery Nova", Address = "Nowy Swiat 45, Warsaw", Capacity = 120, Phone = "333444555" },
                new Venue { VenueId = 4, Name = "City Conference Center", Address = "Zlota 59, Warsaw", Capacity = 300, Phone = "444555666" }
            );

            modelBuilder.Entity<VenueDetails>().HasData(
                new VenueDetails { VenueId = 1, ParkingSpaces = 150, AccessibilityInfo = "Wheelchair ramps and elevator access", WebsiteUrl = "https://nch.example.com" },
                new VenueDetails { VenueId = 2, ParkingSpaces = 0, AccessibilityInfo = "Flat terrain, accessible paths", WebsiteUrl = null },
                new VenueDetails { VenueId = 3, ParkingSpaces = 30, AccessibilityInfo = "Ground floor only, no elevator", WebsiteUrl = "https://test321.example.com" },
                new VenueDetails { VenueId = 4, ParkingSpaces = 200, AccessibilityInfo = "Full accessibility, hearing loop available", WebsiteUrl = "https://test123.example.com" }
            );

            modelBuilder.Entity<Attendee>().HasData(
                new Attendee { AttendeeId = 1, FirstName = "Anna", LastName = "Kowalska", Email = "anna@email.com", Phone = "123456789" },
                new Attendee { AttendeeId = 2, FirstName = "Jan", LastName = "Nowak", Email = "jan@email.com", Phone = "234567891" },
                new Attendee { AttendeeId = 3, FirstName = "Maria", LastName = "Wisniewska", Email = "maria@email.com", Phone = "345678912" },
                new Attendee { AttendeeId = 4, FirstName = "Piotr", LastName = "Zielinski", Email = "piotr@email.com", Phone = "456789123" },
                new Attendee { AttendeeId = 5, FirstName = "Katarzyna", LastName = "Lewandowska", Email = "kasia@email.com", Phone = "567891234" }
            );

            modelBuilder.Entity<Event>().HasData(
                new Event { EventId = 1, VenueId = 1, Name = "Warsaw Jazz Festival", EventDate = new DateTime(2026, 7, 10, 19, 0, 0), TicketPrice = 120.00m, Status = "Scheduled" },
                new Event { EventId = 2, VenueId = 2, Name = "Summer Film Night", EventDate = new DateTime(2026, 7, 15, 21, 0, 0), TicketPrice = 45.00m, Status = "Scheduled" },
                new Event { EventId = 3, VenueId = 3, Name = "Modern Art Exhibition", EventDate = new DateTime(2026, 6, 20, 10, 0, 0), TicketPrice = 30.00m, Status = "Completed" },
                new Event { EventId = 4, VenueId = 4, Name = "Tech Conference 2026", EventDate = new DateTime(2026, 8, 1, 9, 0, 0), TicketPrice = 200.00m, Status = "Scheduled" },
                new Event { EventId = 5, VenueId = 1, Name = "Classical Music Evening", EventDate = new DateTime(2026, 7, 25, 20, 0, 0), TicketPrice = 80.00m, Status = "Cancelled" }
            );

            modelBuilder.Entity<Registration>().HasData(
                new Registration { EventId = 1, AttendeeId = 1, RegisteredAt = new DateTime(2026, 6, 20, 10, 15, 0), SeatNumber = "C14" },
                new Registration { EventId = 1, AttendeeId = 2, RegisteredAt = new DateTime(2026, 6, 21, 8, 30, 0), SeatNumber = null },
                new Registration { EventId = 2, AttendeeId = 3, RegisteredAt = new DateTime(2026, 6, 25, 12, 0, 0), SeatNumber = null },
                new Registration { EventId = 3, AttendeeId = 4, RegisteredAt = new DateTime(2026, 6, 15, 9, 45, 0), SeatNumber = "A1" },
                new Registration { EventId = 4, AttendeeId = 5, RegisteredAt = new DateTime(2026, 7, 1, 14, 20, 0), SeatNumber = "R3" },
                new Registration { EventId = 4, AttendeeId = 1, RegisteredAt = new DateTime(2026, 7, 2, 11, 0, 0), SeatNumber = "R4" }
            );
        }
    }
}
