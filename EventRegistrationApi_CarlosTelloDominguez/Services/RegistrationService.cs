using EventRegistrationApi_CarlosTelloDominguez.DTOs;
using EventRegistrationApi_CarlosTelloDominguez.Models;
using EventRegistrationApi_CarlosTelloDominguez.Data;
using Microsoft.EntityFrameworkCore;

namespace EventRegistrationApi_CarlosTelloDominguez.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly AppDbContext _context;
        public RegistrationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RegistrationDto>?> GetRegistrationsForEventAsync(int eventId)
        {
            var eventExists = await _context.Events.AnyAsync(e => e.EventId == eventId);
            if (!eventExists)
            {
                return null;
            }

            return await _context.Registrations
                .Where(r => r.EventId == eventId)
                .OrderBy(r => r.RegisteredOn)
                .Select(r => new RegistrationDto
                {
                    RegistrationId = r.RegistrationId,
                    EventTitle = r.Event!.Title,
                    StudentName = r.StudentName,
                    Email = r.Email,
                    RegisteredOn = r.RegisteredOn
                })
                .ToListAsync();
        }

        public async Task<RegistrationDto?> GetRegistrationByIdAsync(int registrationId)
        {
            return await _context.Registrations
                .Where(r => r.RegistrationId == registrationId)
                .Select(r => new RegistrationDto
                {
                    RegistrationId = r.RegistrationId,
                    EventTitle = r.Event!.Title,
                    StudentName = r.StudentName,
                    Email = r.Email,
                    RegisteredOn = r.RegisteredOn
                })
                .FirstOrDefaultAsync();
        }

        public async Task<RegistrationDto?> CreateRegistrationAsync(int eventId, CreateRegistrationDto dto)
        {
            var existingEvent = await _context.Events.FindAsync(eventId);
            if (existingEvent == null)
            {
                return null;
            }

            var registration = new Registration
            {
                EventId = eventId,
                StudentName = dto.StudentName,
                Email = dto.Email,
                RegisteredOn = DateTime.Now  
            };

            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();

            return new RegistrationDto
            {
                RegistrationId = registration.RegistrationId,   
                EventTitle = existingEvent.Title,
                StudentName = registration.StudentName,
                Email = registration.Email,
                RegisteredOn = registration.RegisteredOn
            };
        }

        public async Task<bool> DeleteRegistrationAsync(int registrationId)
        {
            var registration = await _context.Registrations.FindAsync(registrationId);
            if (registration == null)
            {
                return false;
            }
            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
