using Event_Management_System.DTO;
using Event_Management_System.Models;
using Microsoft.AspNetCore.Identity;

namespace Event_Management_System.Repository;

public interface IUserRepository
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(string passportId);
    Task<IdentityResult> AddUserAsync(UserDto userDto, string password);
    Task<IdentityResult> UpdateUserAsync(UserDto userDto);
    Task<IdentityResult> DeleteUserAsync(string passportId);
    Task<UserDto?> AuthenticateAsync(string passportId, string password);
}