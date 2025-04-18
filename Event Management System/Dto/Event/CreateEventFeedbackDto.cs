namespace Event_Management_System.Dto.Event;

public class CreateEventFeedbackDto
{
    public int EventId { get; set; }
    public Guid? UserId { get; set; }
    public string Content { get; set; } = null!;
    public int Rating { get; set; }
}