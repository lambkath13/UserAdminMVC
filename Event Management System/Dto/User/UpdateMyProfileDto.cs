namespace Event_Management_System.Dto.User;

public class UpdateMyProfileDto
{
    public string PassportId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public IFormFile? AvatarUrl { get; set; }
    public string Email { get; set; } = null!;
}