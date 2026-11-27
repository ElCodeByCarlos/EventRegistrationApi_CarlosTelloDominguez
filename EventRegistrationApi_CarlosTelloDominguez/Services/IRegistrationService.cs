using EventRegistrationApi_CarlosTelloDominguez.DTOs;

namespace EventRegistrationApi_CarlosTelloDominguez.Services
{
   public interface IRegistrationService
    {
        Task<IEnumerable<RegistrationDto>?> GetRegistrationsForEventAsync(int eventId);
        Task<RegistrationDto?> GetRegistrationByIdAsync(int registrationId);
        Task<RegistrationDto?> CreateRegistrationAsync(int eventId, CreateRegistrationDto dto);
        Task<bool> DeleteRegistrationAsync(int registrationId);
    }
}
