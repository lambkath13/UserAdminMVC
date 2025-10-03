using Microsoft.AspNetCore.Identity;

namespace UserAdminMVC.Models;

public class ApplicationUser : IdentityUser
{
    public string? DisplayName { get; set; }
    public UserStatus Status { get; set; } = UserStatus.Unverified;
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }
}

public enum UserStatus { Unverified = 0, Active = 1, Blocked = 2 }