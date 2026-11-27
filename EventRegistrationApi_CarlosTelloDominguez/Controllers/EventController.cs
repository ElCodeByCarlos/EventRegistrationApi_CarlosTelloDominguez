using EventRegistrationApi_CarlosTelloDominguez.DTOs;
using EventRegistrationApi_CarlosTelloDominguez.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistrationApi_CarlosTelloDominguez.Controllers
{
    // activate API behaviour
    [ApiController]
    [Route("api/[controller]")]   
    public class EventsController : ControllerBase
    {
        // the controller talks to the service layer
        private readonly IEventService _eventService;

        // service through the constructor
        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // return all evetns as EventDto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        // Return true events, like OK events otherwise will get 400 
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

        [HttpPost]
        public async Task<ActionResult<EventDto>> CreateEvent(CreateEventDto dto)
        {
            var createdEvent = await _eventService.CreateEventAsync(dto);
            return CreatedAtAction(nameof(GetEvent), new { id = createdEvent.EventId }, createdEvent);
        }
    }
}