using AutoMapper;
using Event_Management_System.Data;
using Event_Management_System.DTO;
using Event_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Repository;

public class UserRepository(IMapper mapper, AppDbContext context)
    : IUserRepository
{
    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await context.Users.ToListAsync();
        return mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto?> GetByIdAsync(string passportId)
    {
        var user = await context.Users.FirstOrDefaultAsync(x=> x.PassportId == passportId);
        return mapper.Map<UserDto?>(user);
    }

    public async Task<IdentityResult> AddAsync(UserDto userDto, string password)
    {
        var user = mapper.Map<User>(userDto);
         await context.Users.AddAsync(user);
         await context.SaveChangesAsync();
         return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(UserDto userDto)
    {
        var user = mapper.Map<User>(userDto);
        context.Users.Update(user);
        await context.SaveChangesAsync();
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(string passportId)
    {
        var user = await context.Users.FirstOrDefaultAsync(x=> x.PassportId == passportId);
        if (user == null) return IdentityResult.Failed();
        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return IdentityResult.Success;
    }

    public async Task<User?> GetByPassportId(string passportId)
    {
       return await context.Users.FirstOrDefaultAsync(x=> x.PassportId == passportId);
    }
}
