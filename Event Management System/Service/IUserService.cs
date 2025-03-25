using Event_Management_System.DTO;
using Event_Management_System.Models;
using Microsoft.AspNetCore.Identity;

namespace Event_Management_System.Service;

public interface IUserService
{
    Task<IdentityResult> Create(UserDto userDto, string password);
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(string passportId);
    Task<IdentityResult> UpdateAsync(UserDto userDto);
    Task<IdentityResult> DeleteAsync(string passportId);
}
