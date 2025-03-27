using Event_Management_System.DTO;
using Event_Management_System.Models;
using Microsoft.AspNetCore.Identity;

namespace Event_Management_System.Repository;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<IdentityResult> AddAsync(User userEntity, string password);
    Task<IdentityResult> UpdateAsync(User userEntity);
    Task<IdentityResult> DeleteAsync(Guid id);
    Task<User?> GetByPassportId(string loginRequestPassportId);
}