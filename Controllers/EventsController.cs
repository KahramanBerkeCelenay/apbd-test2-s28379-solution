using Test2.DTOs;
using Test2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Test2.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController : ControllerBase
    {
        private readonly IEventsService _eventsService;

        public EventsController(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var eventDto = await _eventsService.GetEventAsync(id);

            if (eventDto is null)
            {
                return NotFound($"Event with id {id} not found");
            }

            return Ok(eventDto);
        }

        [HttpPost("{id}/registrations")]
        public async Task<IActionResult> RegisterAttendee(int id, [FromBody] CreateRegistrationDTO dto)
        {
            var (success, errorMessage, statusCode) = await _eventsService.RegisterAttendeeAsync(id, dto);

            if (!success)
            {
                return StatusCode(statusCode, errorMessage);
            }

            return StatusCode(statusCode);
        }
    }
}
