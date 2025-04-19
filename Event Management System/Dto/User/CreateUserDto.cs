using Event_Management_System.Enums;

namespace Event_Management_System.Dto.User;

public class CreateUserDto
{
    public string PassportId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public IFormFile? AvatarUrl { get; set; }
    public string Password { get; set; }
    public string Email { get; set; } = null!;
    public UserRole Role { get; set; }
}