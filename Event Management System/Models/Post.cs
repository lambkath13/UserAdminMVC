using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Management_System.Models;

public class Post
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(500)]
    public string Content { get; set; } = null!;
    
    [MaxLength(500)]
    public required string ImageUrl { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    
    public List<PostComment> PostComments { get; set; } = null!;
}