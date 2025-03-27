using AutoMapper;
using Event_Management_System.Data;
using Event_Management_System.DTO;
using Event_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Repository;

public class UserRepository( AppDbContext context)
    : IUserRepository
{
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
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

    public async Task<User> GetByName(string name)
    {
        return await context.Users.FirstOrDefaultAsync(x=> x.Name == name);
    }
}
