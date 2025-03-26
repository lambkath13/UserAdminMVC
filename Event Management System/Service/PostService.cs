using AutoMapper;
using Event_Management_System.DTO;
using Event_Management_System.Models;
using Event_Management_System.Repository;

namespace Event_Management_System.Service;

public class PostService:IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;

    public PostService(IPostRepository postRepository, IMapper mapper)
    {
        _mapper = mapper;
        _postRepository = postRepository;
    }

    public async Task<IEnumerable<PostDto>> GetAllAsync()
    {
        var posts= await _postRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PostDto>>(posts);
    }

    public async Task<PostDto?> GetByIdAsync(int id)
    {
        var postEntity = await _postRepository.GetByIdAsync(id);
        return _mapper.Map<PostDto>(postEntity);
    }

    public async Task AddAsync(PostDto postDto)
    {
        var postEntity = _mapper.Map<Post>(postDto);
        await _postRepository.AddAsync(postEntity);
    }

    public async Task UpdateAsync(PostDto postDto)
    {
        var postEntity = _mapper.Map<Post>(postDto);
        await _postRepository.UpdateAsync(postEntity);
    }

    public async Task DeleteAsync(int id)
    {
        await _postRepository.DeleteAsync(id);
    }
}