namespace Event_Management_System.Dto.Post;

public class PostDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? UserId { get; set; }
    public int EventId { get; set; }
    public string UserName { get; set; }
    public string? EventTitle { get; set; }
}