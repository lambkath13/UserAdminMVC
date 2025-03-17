using System.ComponentModel.DataAnnotations;
using Event_Management_System.Enums;
using Microsoft.AspNetCore.Identity;

namespace Event_Management_System.Models
{
    public class User:IdentityUser
    {
        [Key]
        [Required]
        [MaxLength(20)]
        public required string PassportId { get; set; }

        [Required]
        [MaxLength(20)]
        public required string Name { get; set; }
        
        [MaxLength(200)]
        public string? AvatarUrl { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; } =null!;

        [Required]
        public UserRole Role { get; set; }

        public bool IsFirstAdmin { get; set; } = false;

        public List<Event> Events { get; set; } = null!;
        public List<Post> Posts { get; set; } = null!;
    }


}
