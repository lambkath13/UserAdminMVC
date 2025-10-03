using UserAdminMVC.Data;
using UserAdminMVC.Models;

namespace UserAdminMVC.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class UserService : IUserService
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _users;
    private readonly SignInManager<ApplicationUser> _signIn;

    public UserService(AppDbContext db, UserManager<ApplicationUser> users, SignInManager<ApplicationUser> signIn)
    { _db = db; _users = users; _signIn = signIn; }

    public async Task<IReadOnlyList<ApplicationUser>> GetAllAsync() =>
        await _db.Users.OrderByDescending(u => u.LastLoginAt).ToListAsync();

    public async Task<(bool ok, string? error)> BlockAsync(IEnumerable<string> ids)
        => await ApplyAsync(ids, u => u.Status = UserStatus.Blocked);

    public async Task<(bool ok, string? error)> UnblockAsync(IEnumerable<string> ids)
        => await ApplyAsync(ids, u => u.Status = UserStatus.Active);

    public async Task<(bool ok, string? error)> DeleteAsync(IEnumerable<string> ids)
    {
        var toDelete = await _db.Users.Where(u => ids.Contains(u.Id)).ToListAsync();
        _db.Users.RemoveRange(toDelete);
        await _db.SaveChangesAsync();
        return (true, null);
    }

    public async Task<int> DeleteUnverifiedAsync(string? currentUserId = null)
    {
        var targets = await _db.Users
            .Where(u => !u.EmailConfirmed && (currentUserId == null || u.Id != currentUserId))
            .ToListAsync();

        _db.Users.RemoveRange(targets);
        return await _db.SaveChangesAsync();
    }


    public async Task<bool> IsCurrentUserBlockedOrMissingAsync(ClaimsPrincipal principal)
    {
        var me = await _users.GetUserAsync(principal);
        return me == null || me.Status == UserStatus.Blocked;
    }

    private async Task<(bool ok, string? error)> ApplyAsync(IEnumerable<string> ids, Action<ApplicationUser> mut)
    {
        var list = await _db.Users.Where(u => ids.Contains(u.Id)).ToListAsync();
        list.ForEach(mut);
        await _db.SaveChangesAsync();
        return (true, null);
    }
}
