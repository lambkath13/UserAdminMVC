using System.ComponentModel.DataAnnotations;

namespace Event_Management_System.Dto.Post;

public class CreatePostDto
{
    [MinLength(5, ErrorMessage = "Title must be at least 200 characters.")]
    public string Content { get; set; } = null!;
    public IFormFile Image { get; set; } = null!;
    public Guid? UserId { get; set; }
}
