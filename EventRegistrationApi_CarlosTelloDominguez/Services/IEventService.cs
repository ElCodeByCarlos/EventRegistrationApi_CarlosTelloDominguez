using EventRegistrationApi_CarlosTelloDominguez.DTOs;

namespace EventRegistrationApi_CarlosTelloDominguez.Services
{
    public interface IEventService
    {
        Task<IEnumerable<EventDto>> GetAllEventsAsync();
        Task<EventDto?> GetEventByIdAsync(int id);
            
    }
}
