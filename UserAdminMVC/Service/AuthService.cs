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

    public AuthService(UserManager<ApplicationUser> u, SignInManager<ApplicationUser> s, AppDbContext db)
    { _users = u; _signIn = s; _db = db; }

    public async Task<(bool ok, string? error)> RegisterAsync(string email, string password, string displayName)
    {
        var user = new ApplicationUser { UserName = email, Email = email, DisplayName = displayName, Status = UserStatus.Unverified };
        try
        {
            var res = await _users.CreateAsync(user, password);
            if (!res.Succeeded) return (false, string.Join("; ", res.Errors.Select(e => e.Description)));
            await _signIn.SignInAsync(user, isPersistent: false);
            return (true, null);
        }
        catch (DbUpdateException ex) when (ex.InnerException is Microsoft.Data.SqlClient.SqlException sql && (sql.Number == 2627 || sql.Number == 2601))
        {
            return (false, "This e-mail is already taken.");
        }
    }

    public async Task<(bool ok, string? error)> LoginAsync(string email, string password, bool remember)
    {
        // По докам: PasswordSignInAsync(string userName, ...) — используем email как userName
        var res = await _signIn.PasswordSignInAsync(email, password, remember, lockoutOnFailure: false);
        if (!res.Succeeded) return (false, "Invalid credentials.");

        var user = await _users.FindByEmailAsync(email);
        if (user != null)
        {
            user.LastLoginAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }
        return (true, null);
    }

    public Task LogoutAsync() => _signIn.SignOutAsync();
}
