using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserAdminMVC.Service;

[Authorize]
public class UsersController : Controller
{
    private readonly IUserService _svc;
    private readonly IAuthService _auth; // для SignOut

    public UsersController(IUserService svc, IAuthService auth)
    {
        _svc = svc;
        _auth = auth;
    }

    public async Task<IActionResult> Index()
        => View(await _svc.GetAllAsync());

    [HttpGet]
    public async Task<IActionResult> Status()
        => await _svc.IsCurrentUserBlockedOrMissingAsync(User)
            ? Unauthorized(new { code = "blocked" })
            : Ok(new { code = "ok" });

// 2. Block
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Block([FromForm] string[] userIds)
    {
        // подстраховка на сервере: если юзер уже заблокирован/удалён — кик
        if (await _svc.IsCurrentUserBlockedOrMissingAsync(User))
        {
            await _auth.LogoutAsync();
            return RedirectToAction("Login", "Account");
        }

        await _svc.BlockAsync(userIds);
        return RedirectToAction(nameof(Index));
    }

// 3. Unblock
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Unblock([FromForm] string[] userIds)
    {
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
        if (await _svc.IsCurrentUserBlockedOrMissingAsync(User))
        {
            await _auth.LogoutAsync();
            return RedirectToAction("Login", "Account");
        }

        await _svc.DeleteAsync(userIds);

        // если удалил себя → разлогин и редирект на логин
        var meId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (meId != null && userIds.Contains(meId))
        {
            await _auth.LogoutAsync();
            return RedirectToAction("Login", "Account");
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUnverified()
    {
        var meId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var removed = await _svc.DeleteUnverifiedAsync(meId);

        // если удалил себя (Unverified) → разлогин
        if (removed > 0 && meId != null)
        {
            var stillExists = await _svc.IsCurrentUserBlockedOrMissingAsync(User);
            if (stillExists) // себя уже удалили
            {
                await _auth.LogoutAsync();
                return RedirectToAction("Login", "Account");
            }
        }

        return RedirectToAction(nameof(Index));
    }
}
