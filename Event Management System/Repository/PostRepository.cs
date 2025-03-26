using AutoMapper;
using Event_Management_System.Data;
using Event_Management_System.DTO;
using Event_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Repository;

public class PostRepository:IPostRepository
{
    private readonly AppDbContext _context;

    public PostRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        return await _context.Posts.ToListAsync();
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        return await _context.Posts.FindAsync(id);
    }

    public async Task AddAsync(Post postEntity)
    {
        await _context.Posts.AddAsync(postEntity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Post postEntity)
    {
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