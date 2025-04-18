using Event_Management_System.Enums;

namespace Event_Management_System.Dto.Event;

public class UpdateEventDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public EventStatus Status { get; set; }
    public DateTime EventDate { get; set; }
    public string? Location { get; set; }
    public IFormFile? File { get; set; }
}