namespace Event_Management_System.DTO;

public class CreatePostDto
{
    public string Content { get; set; } = null!;
    public IFormFile Image { get; set; } = null!;
    public Guid? UserId { get; set; }
}
