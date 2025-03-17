using Event_Management_System.DTO;
using Event_Management_System.Enums;
using Event_Management_System.Models;
using Event_Management_System.Repository;
using Microsoft.AspNetCore.Identity;

namespace Event_Management_System.Service;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IdentityResult> RegisterUserAsync(UserDto userDto, string password)
    {
        var existingUser = await _userManager.FindByNameAsync(userDto.PassportId);
        if (existingUser != null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User already exists." });
        }
        
        var user = new User
        {
            PassportId = userDto.PassportId,
            Name = userDto.Name
        };
        
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<UserDto?> AuthenticateAsync(string passportId, string password)
    {
        var user = await _userManager.FindByNameAsync(passportId);
        if (user == null)
            return null;
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        return result.Succeeded ? new UserDto { PassportId = user.PassportId, Name = user.Name } : null;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = _userManager.Users.ToList();
        return users.Select(user => new UserDto { PassportId = user.PassportId, Name = user.Name });
    }

    public async Task<UserDto?> GetUserByIdAsync(string passportId)
    {
        var user = await _userManager.FindByNameAsync(passportId);
        return user != null ? new UserDto { PassportId = user.PassportId, Name = user.Name } : null;
    }

    public async Task<IdentityResult> UpdateUserAsync(UserDto userDto)
    {
        var user = await _userManager.FindByNameAsync(userDto.PassportId);
        if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        
        user.Name = userDto.Name;
        return await _userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> DeleteUserAsync(string passportId)
    {
        var user = await _userManager.FindByNameAsync(passportId);
        if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        return await _userManager.DeleteAsync(user);
    }
}
