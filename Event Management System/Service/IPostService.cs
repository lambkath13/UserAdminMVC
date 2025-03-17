using Event_Management_System.DTO;
using Event_Management_System.Models;

namespace Event_Management_System.Service;

public interface IPostService
{
    Task<IEnumerable<PostDto>> GetAllPostsAsync();
    Task<PostDto?> GetPostByIdAsync(int id);
    Task AddPostAsync(PostDto postDto);
    Task UpdatePostAsync(PostDto postDto);
    Task DeletePostAsync(int id);
}
