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
    public string Password { get; set; } = null!;
}
