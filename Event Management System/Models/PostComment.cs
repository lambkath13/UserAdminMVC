using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Management_System.Models;

public class PostComment
{
    [Key]
    public int PostCommentId { get; set; }

    public int? PostId { get; set; }
    public  Post? Post { get; set; }
    
    [MaxLength(20)]
    public required string UserId { get; set; }

    public required User User { get; set; }

    [MaxLength(200)]
    public required string Content { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}