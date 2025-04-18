using Event_Management_System.Enums;

namespace Event_Management_System.DTO;

public class GetAllEventDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public EventStatus Status { get; set; }
    public DateTime EventDate { get; set; }
    public string? Location { get; set; }
    public Guid? OrganizerId { get; set; }
    public string File { get; set; }
    public bool IsSubscribe { get; set; }
}