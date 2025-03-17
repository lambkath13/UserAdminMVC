namespace Event_Management_System.DTO;

public class EventRegistrationDto
{
    public int EventRegistrationId { get; set; }
    public int EventId { get; set; }
    public string UserId { get; set; } = null!;
    public DateTime RegisteredAt { get; set; }
}