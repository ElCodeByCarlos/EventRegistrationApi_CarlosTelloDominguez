using System.ComponentModel.DataAnnotations;
using EventRegistrationApi_CarlosTelloDominguez.Models;
using EventRegistrationApi_CarlosTelloDominguez.Validation;

namespace EventRegistrationApi_CarlosTelloDominguez.DTOs
{
    // I define Client POST request to create an event
    // Events created into the  DTO
    public class CreateEventDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Character are too many")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(300, ErrorMessage = "Character are too many")]
        public string Location { get; set; } = string.Empty;

        [FutureDate(ErrorMessage = "Event date must be in the future.")]
        public DateTime EventDate { get; set; }
        [Range(1, 500, ErrorMessage = "Capacity must be between 1 and 500.")]
        public int Capacity { get; set; }
    }
}
