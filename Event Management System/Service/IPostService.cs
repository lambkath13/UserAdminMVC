using Event_Management_System.Dto.Post;
using Event_Management_System.Models;

namespace Event_Management_System.Service;

public interface IPostService
{
    Task<IEnumerable<PostDto>> GetAllAsync(Guid? userId);
    Task<PostDto?> GetByIdAsync(int id);
    Task AddAsync(PostDto postDto);
    Task UpdateAsync(PostDto postDto);
    Task DeleteAsync(int id);
}
