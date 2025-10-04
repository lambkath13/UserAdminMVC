using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserAdminMVC.Service;

namespace UserAdminMVC.Controllers;

[Authorize]
public class UsersController : Controller
{
    private readonly IUserService _svc;
    private readonly IAuthService _auth; // note: used for logout after self-block or self-delete

    // important: constructor injects services for user operations and authentication
    public UsersController(IUserService svc, IAuthService auth)
    {
        _svc = svc;
        _auth = auth;
    }

    // note: main page with all users (sorted by LastLoginAt)
    public async Task<IActionResult> Index()
        => View(await _svc.GetAllAsync());

    // nota bene: check current user status before any action
    [HttpGet]
    public async Task<IActionResult> Status()
        => await _svc.IsCurrentUserBlockedOrMissingAsync(User)
            ? Unauthorized(new { code = "blocked" })
            : Ok(new { code = "ok" });

    // 2. Block
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Block([FromForm] string[] userIds)
    {
        // Perform blocking of selected users
        await _svc.BlockAsync(userIds);

        // If the current user blocked themselves → logout immediately
        var meId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (meId != null && userIds.Contains(meId))
        {
            await _auth.LogoutAsync();
            return RedirectToAction("Login", "Account");
        }

        // Otherwise, stay on the list page (middleware will handle logout on next action)
        return RedirectToAction(nameof(Index));
    }


    // 3. Unblock
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Unblock([FromForm] string[] userIds)
    {
        // note: verify current user isn't blocked before action
        if (await _svc.IsCurrentUserBlockedOrMissingAsync(User))
        {
            await _auth.LogoutAsync();
            return RedirectToAction("Login", "Account");
        }

        await _svc.UnblockAsync(userIds);
        return RedirectToAction(nameof(Index));
    }

    // 4. Delete
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete([FromForm] string[] userIds)
    {
        // note: ensure user still valid before attempting deletion
        if (await _svc.IsCurrentUserBlockedOrMissingAsync(User))
        {
            await _auth.LogoutAsync();
            return RedirectToAction("Login", "Account");
        }

        await _svc.DeleteAsync(userIds);

        // important: if deleted self → force logout and go to login page
        var meId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (meId != null && userIds.Contains(meId))
        {
            await _auth.LogoutAsync();
            return RedirectToAction("Login", "Account");
        }

        return RedirectToAction(nameof(Index));
    }

    // nota bene: removes all unverified accounts, except current verified one
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUnverified()
    {
        var meId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var removed = await _svc.DeleteUnverifiedAsync(meId);

        // important: if current user also got removed → logout immediately
        if (removed > 0 && meId != null)
        {
            var stillExists = await _svc.IsCurrentUserBlockedOrMissingAsync(User);
            if (stillExists) // self deleted
            {
                await _auth.LogoutAsync();
                return RedirectToAction("Login", "Account");
            }
        }

        return RedirectToAction(nameof(Index));
    }
}
