using Event_Management_System.DTO;
using Event_Management_System.Models;
using Microsoft.AspNetCore.Identity;

namespace Event_Management_System.Repository;

public interface IUserRepository
{
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(string passportId);
    Task<IdentityResult> AddAsync(UserDto userDto, string password);
    Task<IdentityResult> UpdateAsync(UserDto userDto);
    Task<IdentityResult> DeleteAsync(string passportId);
    Task<UserDto?> AuthenticateAsync(string passportId, string password);
}