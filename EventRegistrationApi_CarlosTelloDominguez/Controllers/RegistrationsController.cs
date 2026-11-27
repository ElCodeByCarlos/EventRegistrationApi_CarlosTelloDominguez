using Microsoft.AspNetCore.Mvc;
using EventRegistrationApi_CarlosTelloDominguez.Services;
using EventRegistrationApi_CarlosTelloDominguez.DTOs;

namespace EventRegistrationApi_CarlosTelloDominguez.Controllers
{
    [ApiController]
    [Route("api")]
    public class RegistrationsController : ControllerBase
    {
       private readonly IRegistrationService _registrationService;
        public RegistrationsController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }
        [HttpGet("events/{eventId}/registrations")]
        public async Task<ActionResult<IEnumerable<RegistrationDto>>> GetRegistrationsForEvent(int eventId)
        {
            var registrations = await _registrationService.GetRegistrationsForEventAsync(eventId);
            if (registrations == null)
            {
                return NotFound(new { message = $"No registrations found for event with id {eventId}." });
            }
            return Ok(registrations);
        }
        [HttpGet("registrations/{registrationId}")]
        public async Task<ActionResult<RegistrationDto>> GetRegistrationById(int registrationId)
        {
            var registration = await _registrationService.GetRegistrationByIdAsync(registrationId);
            if (registration == null)
            {
                return NotFound(new { message = $"Registration with id {registrationId} was not found." });
            }
            return Ok(registration);
        }

        [HttpPost("events/{eventId}/registrations")]
        public async Task<ActionResult<RegistrationDto>> CreateRegistration(int eventId, CreateRegistrationDto dto)
        {
            var result = await _registrationService.CreateRegistrationAsync(eventId, dto);

            return result.Status switch
            {
                ServiceResultStatus.Success => Created($"/api/events/{eventId}/registrations", result.Data),
                ServiceResultStatus.NotFound => NotFound(result.ErrorMessage),
                ServiceResultStatus.BadRequest => BadRequest(result.ErrorMessage),
                ServiceResultStatus.Conflict => Conflict(result.ErrorMessage),
                _ => StatusCode(500)
            };
        }
        [HttpDelete("registrations/{registrationId}")]
        public async Task<IActionResult> DeleteRegistration(int registrationId)
        {
            var success = await _registrationService.DeleteRegistrationAsync(registrationId);
            if (!success)
            {
                return NotFound(new { message = $"Registration with id {registrationId} was not found." });
            }
            return NoContent();
        }
    }
}
