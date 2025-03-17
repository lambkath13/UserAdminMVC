using System.ComponentModel.DataAnnotations;
using Event_Management_System.Enums;

namespace Event_Management_System.DTO;

public class RegisterDto
{
    [Required]
    public string PassportId { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;
    
    [Required]
    public UserRole Role  { get; set; }

    [Required]
    public string Password { get; set; } = null!;

    public string? AdminPassword { get; set; } // Только для админов
}
