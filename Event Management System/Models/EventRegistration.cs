using System.ComponentModel.DataAnnotations;

namespace Event_Management_System.Models;
public class EventRegistration
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int EventId { get; set; }
    public Event Event { get; set; } = null!;

    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
}
