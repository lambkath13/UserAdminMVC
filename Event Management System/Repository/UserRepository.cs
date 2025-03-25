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
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await context.Users.ToListAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto?> GetByIdAsync(string passportId)
    {
        var user = await context.Users.FirstOrDefaultAsync(x=> x.PassportId == passportId);
        return _mapper.Map<UserDto?>(user);
    }

    public async Task<IdentityResult> AddAsync(UserDto userDto, string password)
    {
        var user = _mapper.Map<User>(userDto);
         await context.Users.AddAsync(user);
         await context.SaveChangesAsync();
         return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
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

    public async Task<UserDto?> AuthenticateAsync(string passportId, string password)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.PassportId == passportId);
        return user == null ? null : mapper.Map<UserDto>(user);
    }
}
