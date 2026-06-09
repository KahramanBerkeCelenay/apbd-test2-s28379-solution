using Test2.DTOs;

namespace Test2.Services
{
    public interface IEventsService
    {
        Task<EventDTO?> GetEventAsync(int id);

        Task<(bool Success, string? ErrorMessage, int StatusCode)> RegisterAttendeeAsync(int eventId, CreateRegistrationDTO dto);
    }
}
