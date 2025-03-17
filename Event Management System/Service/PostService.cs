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

    public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
    {
        return await _postRepository.GetAllPostAsync();
    }

    public async Task<PostDto?> GetPostByIdAsync(int id)
    {
        return await _postRepository.GetPostByIdAsync(id);
    }

    public async Task AddPostAsync(PostDto postDto)
    {
        await _postRepository.AddPostAsync(postDto);
    }

    public async Task UpdatePostAsync(PostDto postDto)
    {
        await _postRepository.UpdatePostAsync(postDto);
    }

    public async Task DeletePostAsync(int id)
    {
        await _postRepository.DeletePostAsync(id);
    }
}