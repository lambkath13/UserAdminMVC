using System.ComponentModel.DataAnnotations;

namespace Event_Management_System.Models;

public class Image
{
    [Key]
    public int ImageId { get; set; }

    public int? PostId { get; set; } 
    public Post? Post { get; set; } 
}