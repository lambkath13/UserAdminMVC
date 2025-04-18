using Event_Management_System.Dto.User;
using Event_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Service;

public interface IUserService
{
    Task<IdentityResult> AddAsync(User user);
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(Guid? id);
    Task<IdentityResult> UpdateAsync(UserDto userDto);
    Task<IdentityResult> DeleteAsync(Guid id);
    Task<UserDto> GetByPassportIdAsync(string loginRequestPassportId );
    Task<UserDto> GetByName(string name);
}
