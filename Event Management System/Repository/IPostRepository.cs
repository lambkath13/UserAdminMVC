using Event_Management_System.Enums;
using Event_Management_System.Models;

namespace Event_Management_System.Repository;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetAllAsync(Guid? userId, UserRole? role);
    Task<Post?> GetByIdAsync(int id);
    Task AddAsync(Post postEntity);
    Task UpdateAsync(Post postEntity);
    Task DeleteAsync(int id);
}
