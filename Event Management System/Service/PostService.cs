using AutoMapper;
using Event_Management_System.Dto.Post;
using Event_Management_System.Models;
using Event_Management_System.Repository;

namespace Event_Management_System.Service;

public class PostService(IPostRepository postRepository, IMapper mapper, IUserRepository userRepository) : IPostService
{
    public async Task<IEnumerable<PostDto>> GetAllAsync(Guid? userId, string? query)
    {
        var user = await userRepository.GetByIdAsync(userId);
        
        var posts= await postRepository.GetAllAsync(userId, user?.Role, query);
        return mapper.Map<IEnumerable<PostDto>>(posts);
    }

    public async Task<PostDto?> GetByIdAsync(int id)
    {
        var postEntity = await postRepository.GetByIdAsync(id);
        return mapper.Map<PostDto>(postEntity);
    }

    public async Task AddAsync(PostDto postDto)
    {
        var postEntity = mapper.Map<Post>(postDto);
        await postRepository.AddAsync(postEntity);
    }

    public async Task UpdateAsync(PostDto postDto)
    {
        var postEntity = mapper.Map<Post>(postDto);
        await postRepository.UpdateAsync(postEntity);
    }

    public async Task DeleteAsync(int id)
    {
        await postRepository.DeleteAsync(id);
    }
}