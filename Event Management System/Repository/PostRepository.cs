using AutoMapper;
using Event_Management_System.Data;
using Event_Management_System.Enums;
using Event_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Repository;

public class PostRepository(AppDbContext context) : IPostRepository
{
    public async Task<IEnumerable<Post>> GetAllAsync(Guid? userId, UserRole? role, string? query)
    {
        query = query?.ToLower();

        return await context.Posts
            .Where(x =>
                (
                    (userId != null && x.UserId == userId) ||
                    (role != null && role == UserRole.Admin)
                )
                &&
                (
                    string.IsNullOrEmpty(query) ||
                    x.Content.ToLower().Contains(query)
                )
            )
            .ToListAsync();
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        return await context.Posts.FindAsync(id);
    }

    public async Task AddAsync(Post postEntity)
    {
        await context.Posts.AddAsync(postEntity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Post postEntity)
    {
        context.Posts.Update(postEntity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var postEntity = await context.Posts.FindAsync(id);
        if (postEntity != null)
        {
            context.Posts.Remove(postEntity);
            await context.SaveChangesAsync();
        }
    }
}