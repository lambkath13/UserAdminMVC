namespace Event_Management_System.Dto.Post;

public class PostDto
{
    public int PostId { get; set; }
    public string Content { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? UserId { get; set; }
}