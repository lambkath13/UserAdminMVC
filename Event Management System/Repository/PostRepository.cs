using AutoMapper;
using Event_Management_System.Data;
using Event_Management_System.DTO;
using Event_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Repository;

public class PostRepository:IPostRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public PostRepository(AppDbContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PostDto>> GetAllAsync()
    {
        var posts = await _context.Posts.ToListAsync();
        return _mapper.Map<IEnumerable<PostDto>>(posts);
    }

    public async Task<PostDto?> GetByIdAsync(int id)
    {
        var posts = await _context.Posts.FindAsync(id);
        return _mapper.Map<PostDto?>(posts);
    }

    public async Task AddAsync(PostDto postDto)
    {
        var postEntity = _mapper.Map<Post>(postDto);
        await _context.Posts.AddAsync(postEntity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PostDto postDto)
    {
        var postEntity = _mapper.Map<Post>(postDto);
        _context.Posts.Update(postEntity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var postEntity = await _context.Posts.FindAsync(id);
        if (postEntity != null)
        {
            _context.Posts.Remove(postEntity);
            await _context.SaveChangesAsync();
        }
    }
}