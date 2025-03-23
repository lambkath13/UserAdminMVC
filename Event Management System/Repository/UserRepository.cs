using AutoMapper;
using Event_Management_System.DTO;
using Event_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Repository;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;

    public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto?> GetByIdAsync(string passportId)
    {
        var user = await _userManager.FindByNameAsync(passportId);
        return _mapper.Map<UserDto?>(user);
    }

    public async Task<IdentityResult> AddAsync(UserDto userDto, string password)
    {
        var user = _mapper.Map<User>(userDto);
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<IdentityResult> UpdateAsync(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        return await _userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> DeleteAsync(string passportId)
    {
        var user = await _userManager.FindByNameAsync(passportId);
        if (user == null) return IdentityResult.Failed();
        return await _userManager.DeleteAsync(user);
    }

    public async Task<UserDto?> AuthenticateAsync(string passportId, string password)
    {
        var user = await _userManager.FindByNameAsync(passportId);
        if (user == null) return null;

        var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
        return result.Succeeded ? _mapper.Map<UserDto>(user) : null;
    }
}
