namespace Event_Management_System.DTO;

public class CreatePostDto
{
    public string Content { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string UserId { get; set; } = null!;
}
