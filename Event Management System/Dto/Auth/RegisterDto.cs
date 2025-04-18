using System.ComponentModel.DataAnnotations;

namespace Event_Management_System.Dto.Auth;

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
