namespace Event_Management_System.DTO;

public class CreateEventDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime EventDate { get; set; }
    public string? Location { get; set; }
    public IFormFile File { get; set; }
    public Guid? OrganizerId { get; set; }
    public int Status { get; set; }
}