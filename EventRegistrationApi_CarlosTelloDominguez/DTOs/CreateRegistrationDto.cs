using System.ComponentModel.DataAnnotations;

namespace EventRegistrationApi_CarlosTelloDominguez.DTOs
{
    public class CreateRegistrationDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage="name most be between 1 to 50 characters")]
        public string StudentName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;
    }
}
