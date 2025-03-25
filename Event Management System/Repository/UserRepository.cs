using AutoMapper;
using Event_Management_System.Data;
using Event_Management_System.DTO;
using Event_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Repository;

public class UserRepository(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, AppDbContext context)
    : IUserRepository
{
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await userManager.Users.ToListAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto?> GetByIdAsync(string passportId)
    {
        var user = await userManager.FindByNameAsync(passportId);
        return _mapper.Map<UserDto?>(user);
    }

    public async Task<IdentityResult> AddAsync(UserDto userDto, string password)
    {
        var user = _mapper.Map<User>(userDto);
        return await userManager.CreateAsync(user, password);
    }

    public async Task<IdentityResult> UpdateAsync(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        return await userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> DeleteAsync(string passportId)
    {
        var user = await userManager.FindByNameAsync(passportId);
        if (user == null) return IdentityResult.Failed();
        return await userManager.DeleteAsync(user);
    }

    public async Task<UserDto?> AuthenticateAsync(string passportId, string password)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.PassportId == passportId);
        return user == null ? null : mapper.Map<UserDto>(user);
    }
}
