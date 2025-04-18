namespace Event_Management_System.Dto.Event;

public class EventFeedbackDto
{
    public int EventFeedbackId { get; set; }
    public int EventId { get; set; }
    public string UserId { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
}