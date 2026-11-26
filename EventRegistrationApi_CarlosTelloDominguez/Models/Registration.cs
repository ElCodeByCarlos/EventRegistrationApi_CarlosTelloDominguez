using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventRegistrationApi_CarlosTelloDominguez.Models
{
    public class Registration
    {
        [Key]
        public int RegistrationId { get; set; }

        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }

        [Required]
        public string StudentName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        public DateTime RegisteredOn { get; set; }

        public Event? Event { get; set; }
    }
}
