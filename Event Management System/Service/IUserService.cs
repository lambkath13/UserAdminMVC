using Event_Management_System.DTO;
using Event_Management_System.Models;
using Microsoft.AspNetCore.Identity;

namespace Event_Management_System.Service;

public interface IUserService
{
    Task<IdentityResult> RegisterUserAsync(UserDto userDto, string password);
    Task<UserDto?> AuthenticateAsync(string passportId, string password);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(string passportId);
    Task<IdentityResult> UpdateUserAsync(UserDto userDto);
    Task<IdentityResult> DeleteUserAsync(string passportId);
}
