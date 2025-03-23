using Event_Management_System.DTO;
using Event_Management_System.Models;
using Event_Management_System.Repository;

namespace Event_Management_System.Service;

public class PostService:IPostService
{
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<IEnumerable<PostDto>> GetAllAsync()
    {
        return await _postRepository.GetAllAsync();
    }

    public async Task<PostDto?> GetByIdAsync(int id)
    {
        return await _postRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(PostDto postDto)
    {
        await _postRepository.AddAsync(postDto);
    }

    public async Task UpdateAsync(PostDto postDto)
    {
        await _postRepository.UpdateAsync(postDto);
    }

    public async Task DeleteAsync(int id)
    {
        await _postRepository.DeleteAsync(id);
    }
}