using Test2.Data;
using Test2.DTOs;
using Test2.Entities;
using Microsoft.EntityFrameworkCore;

namespace Test2.Services
{
    public class EventsService : IEventsService
    {
        private readonly AppDbContext _context;

        public EventsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EventDTO?> GetEventAsync(int id)
        {
            return await _context.Events
                .Where(e => e.EventId == id)
                .Select(e => new EventDTO
                {
                    EventId = e.EventId,
                    Name = e.Name,
                    EventDate = e.EventDate,
                    TicketPrice = e.TicketPrice,
                    Status = e.Status,
                    Venue = new VenueDTO
                    {
                        Name = e.Venue.Name,
                        Address = e.Venue.Address,
                        Capacity = e.Venue.Capacity,
                        Phone = e.Venue.Phone,
                        Details = new VenueDetailsDTO
                        {
                            ParkingSpaces = e.Venue.VenueDetails!.ParkingSpaces,
                            AccessibilityInfo = e.Venue.VenueDetails.AccessibilityInfo,
                            WebsiteUrl = e.Venue.VenueDetails.WebsiteUrl
                        }
                    },
                    Registrations = e.Registrations.Select(r => new RegistrationDTO
                    {
                        RegisteredAt = r.RegisteredAt,
                        SeatNumber = r.SeatNumber,
                        Attendee = new AttendeeDTO
                        {
                            FirstName = r.Attendee.FirstName,
                            LastName = r.Attendee.LastName,
                            Email = r.Attendee.Email,
                            Phone = r.Attendee.Phone
                        }
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<(bool Success, string? ErrorMessage, int StatusCode)> RegisterAttendeeAsync(int eventId, CreateRegistrationDTO dto)
        {
            var eventEntity = await _context.Events
                .FirstOrDefaultAsync(e => e.EventId == eventId);

            if (eventEntity is null)
            {
                return (false, $"Event with id {eventId} not found", StatusCodes.Status404NotFound);
            }

            if (eventEntity.Status == "Cancelled")
            {
                return (false, "Cannot register for a cancelled event", StatusCodes.Status400BadRequest);
            }

            if (eventEntity.EventDate < DateTime.Now)
            {
                return (false, "Cannot register for an event that has already taken place", StatusCodes.Status400BadRequest);
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var attendee = new Attendee
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Phone = dto.Phone
                };

                _context.Attendees.Add(attendee);
                await _context.SaveChangesAsync();

                var registration = new Registration
                {
                    EventId = eventId,
                    AttendeeId = attendee.AttendeeId,
                    RegisteredAt = DateTime.Now,
                    SeatNumber = dto.SeatNumber
                };

                _context.Registrations.Add(registration);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return (true, null, StatusCodes.Status201Created);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
