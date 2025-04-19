using AutoMapper;
using Event_Management_System.Data;
using Event_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Repository;

public class UserRepository( AppDbContext context)
    : IUserRepository
{
    public async Task<(List<User>, int)> GetAllAsync(string? query, int pageNumber = 1, int pageSize = 12)
    {
        var queryable = context.Users.AsQueryable();
        if (!string.IsNullOrWhiteSpace(query))
        {
            var loweredQuery = query.ToLower();
            queryable = queryable.Where(x =>
                x.Name.ToLower().Contains(loweredQuery)
            );
        }
        
        var users = await queryable
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (users, await queryable.CountAsync());
    }

    public async Task<User?> GetByIdAsync(Guid? id)
    {
        if (id == null) return null;
        
        return await context.Users.FirstOrDefaultAsync(x=> x.Id == id);
    }

    public async Task<IdentityResult> AddAsync(User userEntity)
    {
         await context.Users.AddAsync(userEntity);
         await context.SaveChangesAsync();
         return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(User userEntity)
    {
        context.Users.Update(userEntity);
        await context.SaveChangesAsync();
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(User user)
    {
        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return IdentityResult.Success;
    }

    public async Task<User?> GetByPassportId(string passportId)
    {
       return await context.Users.FirstOrDefaultAsync(x=> x.PassportId == passportId);
    }

    public async Task<User?> GetByName(string name)
    {
        return await context.Users.FirstOrDefaultAsync(x=> x.Name == name);
    }

    public Task<User?> GetByEmail(string userEmail)
    {
        return context.Users.FirstOrDefaultAsync(x=> x.Email == userEmail);
    }
}
