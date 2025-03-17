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

    public async Task<IEnumerable<PostDto>> GetAllPostAsync()
    {
        var posts = await _context.Posts.ToListAsync();
        return _mapper.Map<IEnumerable<PostDto>>(posts);
    }

    public async Task<PostDto?> GetPostByIdAsync(int id)
    {
        var posts = await _context.Posts.FindAsync(id);
        return _mapper.Map<PostDto?>(posts);
    }

    public async Task AddPostAsync(PostDto postDto)
    {
        var postEntity = _mapper.Map<Post>(postDto);
        await _context.Posts.AddAsync(postEntity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePostAsync(PostDto postDto)
    {
        var postEntity = _mapper.Map<Post>(postDto);
        _context.Posts.Update(postEntity);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePostAsync(int id)
    {
        var postEntity = await _context.Posts.FindAsync(id);
        if (postEntity != null)
        {
            _context.Posts.Remove(postEntity);
            await _context.SaveChangesAsync();
        }
    }
}