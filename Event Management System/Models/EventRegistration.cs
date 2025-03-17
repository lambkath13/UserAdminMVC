using System.ComponentModel.DataAnnotations;

namespace Event_Management_System.Models;
public class EventRegistration
{
    [Key]
    public int EventRegistrationId { get; set; }

    [Required]
    public int EventId { get; set; }
    public Event Event { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;

    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
}
