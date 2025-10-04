using UserAdminMVC.Data;
using UserAdminMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace UserAdminMVC.Service;

public class UserService : IUserService
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _users;
    private readonly SignInManager<ApplicationUser> _signIn;

    // note: inject EF context and Identity managers for all user operations
    public UserService(AppDbContext db, UserManager<ApplicationUser> users, SignInManager<ApplicationUser> signIn)
    { 
        _db = db; 
        _users = users; 
        _signIn = signIn; 
    }

    // important: returns all users ordered by last login time (descending)
    public async Task<IReadOnlyList<ApplicationUser>> GetAllAsync() =>
        await _db.Users.OrderByDescending(u => u.LastLoginAt).ToListAsync();

    // note: blocks selected users by changing status to Blocked
    public async Task<(bool ok, string? error)> BlockAsync(IEnumerable<string> ids)
        => await ApplyAsync(ids, u => u.Status = UserStatus.Blocked);

    // note: unblocks selected users by changing status to Active
    public async Task<(bool ok, string? error)> UnblockAsync(IEnumerable<string> ids)
        => await ApplyAsync(ids, u => u.Status = UserStatus.Active);

    // important: deletes selected users from database completely (not soft delete)
    public async Task<(bool ok, string? error)> DeleteAsync(IEnumerable<string> ids)
    {
        var toDelete = await _db.Users.Where(u => ids.Contains(u.Id)).ToListAsync();
        _db.Users.RemoveRange(toDelete);
        await _db.SaveChangesAsync();
        return (true, null);
    }

    // nota bene: removes all unverified users (except current one if passed)
    public async Task<int> DeleteUnverifiedAsync(string? currentUserId = null)
    {
        var targets = await _db.Users
            .Where(u => !u.EmailConfirmed && (currentUserId == null || u.Id != currentUserId))
            .ToListAsync();

        _db.Users.RemoveRange(targets);
        return await _db.SaveChangesAsync();
    }

    // important: returns true if user not found or blocked (used for “kick on action”)
    public async Task<bool> IsCurrentUserBlockedOrMissingAsync(ClaimsPrincipal principal)
    {
        var me = await _users.GetUserAsync(principal);
        return me == null || me.Status == UserStatus.Blocked;
    }

    // note: reusable helper that applies any action (Block/Unblock) to multiple users
    private async Task<(bool ok, string? error)> ApplyAsync(IEnumerable<string> ids, Action<ApplicationUser> mut)
    {
        var list = await _db.Users.Where(u => ids.Contains(u.Id)).ToListAsync();
        list.ForEach(mut);
        await _db.SaveChangesAsync();
        return (true, null);
    }
}
