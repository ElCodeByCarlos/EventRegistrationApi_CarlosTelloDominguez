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

        /*  public async Task<RegistrationDto?> CreateRegistrationAsync(int eventId, CreateRegistrationDto dto)
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
          }*/

        public async Task<ServiceResult<RegistrationDto>> CreateRegistrationAsync(int eventId, CreateRegistrationDto dto)
        {
            // Rule 1: Event must exist → 404
            var existingEvent = await _context.Events.FindAsync(eventId);
            if (existingEvent == null)
            {
                return ServiceResult<RegistrationDto>.NotFound($"Event with id {eventId} was not found.");
            }

            // Rule 2: No past events → 400
            if (existingEvent.EventDate <= DateTime.Now)
            {
                return ServiceResult<RegistrationDto>.BadRequest("Registration is closed for past events.");
            }

            // Rule 3: No duplicates (case-insensitive) → 409
            var normalizedEmail = dto.Email.Trim().ToLower();
            var alreadyRegistered = await _context.Registrations
                .AnyAsync(r => r.EventId == eventId && r.Email.ToLower() == normalizedEmail);

            if (alreadyRegistered)
            {
                return ServiceResult<RegistrationDto>.Conflict("This email is already registered for this event.");
            }

            // Rule 4: Capacity → 409
            var registeredCount = await _context.Registrations.CountAsync(r => r.EventId == eventId);
            if (registeredCount >= existingEvent.Capacity)
            {
                return ServiceResult<RegistrationDto>.Conflict("This event is full.");
            }

            // All rules passed → create the registration
            var registration = new Registration
            {
                EventId = eventId,
                StudentName = dto.StudentName.Trim(),
                Email = dto.Email.Trim(),
                RegisteredOn = DateTime.Now
            };

            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();

            return ServiceResult<RegistrationDto>.Success(new RegistrationDto
            {
                RegistrationId = registration.RegistrationId,
                EventTitle = existingEvent.Title,
                StudentName = registration.StudentName,
                Email = registration.Email,
                RegisteredOn = registration.RegisteredOn
            });
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
