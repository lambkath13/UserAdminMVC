using Event_Management_System.DTO;
using Event_Management_System.Models;

namespace Event_Management_System.Repository;

public interface IPostRepository
{
    Task<IEnumerable<PostDto>> GetAllAsync();
    Task<PostDto?> GetByIdAsync(int id);
    Task AddAsync(PostDto postDto);
    Task UpdateAsync(PostDto postDto);
    Task DeleteAsync(int id);
}
