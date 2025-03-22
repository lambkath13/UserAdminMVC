using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Management_System.Models;

public class PostComment
{
    [Key]
    public int Id { get; set; }

    public int? PostId { get; set; }
    public  Post? Post { get; set; }
    
    public required Guid UserId { get; set; }

    public required User User { get; set; }

    [MaxLength(200)]
    public required string Content { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}