using System.ComponentModel.DataAnnotations;


namespace Event_Management_System.Models;

public class EventFeedback
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int EventId { get; set; }

    public Event Event { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }

    [MaxLength(100)]
    public string Content { get; set; } = null!;

    public int Rating { get; set; }

    public DateTime CreatedAt { get; set; }
}
