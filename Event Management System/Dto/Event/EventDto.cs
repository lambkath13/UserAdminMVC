using Event_Management_System.Dto.Post;
using Event_Management_System.Enums;

namespace Event_Management_System.Dto.Event;

public class EventDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public EventStatus Status { get; set; }
    public DateTime EventDate { get; set; }
    public string? Location { get; set; }
    public Guid? OrganizerId { get; set; }
    public string File { get; set; }
    public Guid? UserId { get; set; }
    public List<EventUserDto>? Users { get; set; }
    public List<EventFeedbackDto>? Feedbacks{ get; set; }
    public List<PostDto>? Posts { get; set; }
}

public class EventUserDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public DateTime RegisteredAt { get; set; }
}
