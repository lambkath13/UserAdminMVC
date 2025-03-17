using System.ComponentModel.DataAnnotations;

namespace Event_Management_System.Models;

public class LoginRequest
{
    [Key]
    public required string PassportId { get; set; }
    public required string Password { get; set; }
    [MaxLength(50)]
    public  string? AdminPassword { get; set; } // Только для админов
}