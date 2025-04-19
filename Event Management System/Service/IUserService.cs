using Event_Management_System.Dto.User;
using Event_Management_System.Models;
using Microsoft.AspNetCore.Identity;

namespace Event_Management_System.Service;

public interface IUserService
{
    Task<int> AddAsync(User user);
    Task<(List<UserDto>, int)> GetAllAsync(string? query, int pageNumber = 1, int pageSize = 12);
    Task<UserDto?> GetByIdAsync(Guid? id);
    Task<IdentityResult> UpdateAsync(UserDto userDto);
    Task<IdentityResult> DeleteAsync(Guid id);
    Task<UserDto?> GetByPassportIdAsync(string loginRequestPassportId );
    Task<UserDto> GetByName(string name);
}
