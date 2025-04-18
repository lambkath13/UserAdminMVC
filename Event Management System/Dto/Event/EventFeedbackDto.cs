namespace Event_Management_System.Dto.Event;

public class EventFeedbackDto
{
    public string Content { get; set; } = null!;
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
    public GetEventFeedbackUserDto User { get; set; }
}

public class GetEventFeedbackUserDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
}