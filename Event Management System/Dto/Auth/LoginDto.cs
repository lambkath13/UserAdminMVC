using System.ComponentModel.DataAnnotations;

namespace Event_Management_System.Dto.Auth;

public class LoginRequest
{
    [Required]
    public string PassportId { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
