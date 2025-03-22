using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Management_System.Models;

public class EventFeedback
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int EventId { get; set; }

    public Event Event { get; set; } = null!;

    [Required]
    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    [MaxLength(50)]
    public string Content { get; set; } = null!;

    public int Rating { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
