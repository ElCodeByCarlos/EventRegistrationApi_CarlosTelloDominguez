using EventRegistrationApi_CarlosTelloDominguez.DTOs;

namespace EventRegistrationApi_CarlosTelloDominguez.Services
{
    public interface IEventService
    {
        // Returns all events, sorted by date, with RegisteredCount and SpotsLeft calculated.
        Task<IEnumerable<EventDto>> GetAllEventsAsync();
        // Returns a specific event by its ID, with RegisteredCount and SpotsLeft calculated.
        Task<EventDto?> GetEventByIdAsync(int id);
        // Create new event from the client request, and return the created event as EventDto
        Task<EventDto> CreateEventAsync(CreateEventDto dto);
    }
}
