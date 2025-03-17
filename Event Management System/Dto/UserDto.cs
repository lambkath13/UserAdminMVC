using Event_Management_System.Enums;

namespace Event_Management_System.DTO;

public class UserDto
{
    public string PassportId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? AvatarUrl { get; set; }
    public string Email { get; set; } = null!;
    public UserRole Role { get; set; }
}
