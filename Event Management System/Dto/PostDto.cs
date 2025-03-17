namespace Event_Management_System.DTO;

public class PostDto
{
    public int PostId { get; set; }
    public string Content { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string UserId { get; set; } = null!;
}