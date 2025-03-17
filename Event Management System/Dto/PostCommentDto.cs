namespace Event_Management_System.DTO;

public class PostCommentDto
{
    public int PostCommentId { get; set; }
    public int? PostId { get; set; }
    public string UserId { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}