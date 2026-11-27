using EventRegistrationApi_CarlosTelloDominguez.DTOs;
using EventRegistrationApi_CarlosTelloDominguez.Data;
using Microsoft.EntityFrameworkCore;
using EventRegistrationApi_CarlosTelloDominguez.Models;

namespace EventRegistrationApi_CarlosTelloDominguez.Services
{
    // Implements the IEventService interface to provide event-related operations
    public class EventService : IEventService
    {
        private readonly AppDbContext _context;

        public EventService(AppDbContext context)
        {
            _context = context;
        }

        // Return all the events , sorted by date
        public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
        {
            return await _context.Events
                .OrderBy(e => e.EventDate)
                .Select(e => new EventDto
                {
                    EventId = e.EventId,
                    Title = e.Title,
                    Location = e.Location,
                    EventDate = e.EventDate,
                    Capacity = e.Capacity,
                    RegisteredCount = e.Registrations.Count,
                    SpotsLeft = e.Capacity - e.Registrations.Count
                })
                .ToListAsync();
        }
        public async Task<EventDto?> GetEventByIdAsync(int id)
        {
            return await _context.Events
              .Where(e => e.EventId == id)
                .Select(e => new EventDto
                {
                    EventId = e.EventId,
                    Title = e.Title,
                    Location = e.Location,
                    EventDate = e.EventDate,
                    Capacity = e.Capacity,
                    RegisteredCount = e.Registrations.Count,
                    SpotsLeft = e.Capacity - e.Registrations.Count
                })
                .FirstOrDefaultAsync();
        }

        public async Task<EventDto> CreateEventAsync(CreateEventDto dto)
        {
            var newEvent = new Event
            {
                Title = dto.Title,
                Location = dto.Location,
                EventDate = dto.EventDate,
                Capacity = dto.Capacity
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            return new EventDto
            {
                EventId = newEvent.EventId,
                Title = newEvent.Title,
                Location = newEvent.Location,
                EventDate = newEvent.EventDate,
                Capacity = newEvent.Capacity,
                RegisteredCount = 0,
                SpotsLeft = newEvent.Capacity
            };
        }
    }
}
