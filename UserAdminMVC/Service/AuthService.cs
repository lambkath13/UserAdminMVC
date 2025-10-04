using UserAdminMVC.Data;
using UserAdminMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace UserAdminMVC.Service;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _users;
    private readonly SignInManager<ApplicationUser> _signIn;
    private readonly AppDbContext _db;

    // note: injects core Identity services and DbContext
    public AuthService(UserManager<ApplicationUser> u, SignInManager<ApplicationUser> s, AppDbContext db)
    { 
        _users = u; 
        _signIn = s; 
        _db = db; 
    }

    // important: registration creates new user with default status = Unverified
    public async Task<(bool ok, string? error)> RegisterAsync(string email, string password, string displayName)
    {
        var user = new ApplicationUser 
        { 
            UserName = email, 
            Email = email, 
            DisplayName = displayName, 
            Status = UserStatus.Unverified 
        };

        try
        {
            // note: CreateAsync handles password validation per Identity policy
            var res = await _users.CreateAsync(user, password);
            if (!res.Succeeded) 
                return (false, string.Join("; ", res.Errors.Select(e => e.Description)));

            // nota bene: login immediately after successful registration
            await _signIn.SignInAsync(user, isPersistent: false);
            return (true, null);
        }
        // important: catch SQL exception 2627 / 2601 → unique index violation
        catch (DbUpdateException ex) when (ex.InnerException is Microsoft.Data.SqlClient.SqlException sql && (sql.Number == 2627 || sql.Number == 2601))
        {
            return (false, "This e-mail is already taken.");
        }
    }

    // note: login checks user credentials and updates LastLoginAt timestamp
    public async Task<(bool ok, string? error)> LoginAsync(string email, string password, bool remember)
    {
        // note: try to find user by e-mail first
        var user = await _users.FindByEmailAsync(email);
        if (user == null)
        {
            // important: return clear message if user not found
            return (false, "User with this e-mail is not registered.");
        }

        // note: attempt to sign in using Identity's PasswordSignInAsync
        var res = await _signIn.PasswordSignInAsync(email, password, remember, lockoutOnFailure: false);
        if (!res.Succeeded)
        {
            // important: wrong password or account blocked (Identity check fails)
            return (false, "Invalid password or account blocked.");
        }

        // important: update last login timestamp for successful login
        user.LastLoginAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        return (true, null);
    }


    // note: clears authentication cookie and session
    public Task LogoutAsync() => _signIn.SignOutAsync();
}
