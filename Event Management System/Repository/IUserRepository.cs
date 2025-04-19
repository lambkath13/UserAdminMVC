using Event_Management_System.Models;
using Microsoft.AspNetCore.Identity;

namespace Event_Management_System.Repository;

public interface IUserRepository
{
    Task<(List<User>, int)> GetAllAsync(string? query, int pageNumber = 1, int pageSize = 12);
    Task<User?> GetByIdAsync(Guid? id);
    Task<IdentityResult> AddAsync(User userEntity);
    Task<IdentityResult> UpdateAsync(User user);
    Task<IdentityResult> DeleteAsync(User user);
    Task<User?> GetByPassportId(string loginRequestPassportId);
    Task<User> GetByName(string name);
    Task<User?> GetByEmail(string userEmail);
} 