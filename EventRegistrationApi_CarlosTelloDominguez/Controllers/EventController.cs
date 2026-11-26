using EventRegistrationApi_CarlosTelloDominguez.DTOs;
using EventRegistrationApi_CarlosTelloDominguez.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistrationApi_CarlosTelloDominguez.Controllers
{
    public class EventController : Controller
    {
        [ApiController]
        [Route("api/[controller]")]   // → /api/events
        public class EventsController : ControllerBase
        {
            private readonly IEventService _eventService;

            public EventsController(IEventService eventService)
            {
                _eventService = eventService;
            }

            // GET: api/events
            [HttpGet]
            public async Task<ActionResult<IEnumerable<EventDto>>> GetEvents()
            {
                var events = await _eventService.GetAllEventsAsync();
                return Ok(events);
            }

            // GET: api/events/3
            [HttpGet("{id}")]
            public async Task<ActionResult<EventDto>> GetEvent(int id)
            {
                var eventDto = await _eventService.GetEventByIdAsync(id);

                if (eventDto == null)
                {
                    return NotFound(new { message = $"Event with id {id} was not found." });
                }

                return Ok(eventDto);
            }
        }
    }
}
