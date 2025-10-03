using Microsoft.AspNetCore.Mvc;
using UserAdminMVC.Service;

public class AccountController : Controller
{
    private readonly IAuthService _auth;
    public AccountController(IAuthService auth) => _auth = auth;

    [HttpGet] public IActionResult Register() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(string email, string password, string displayName)
    {
        var (ok, err) = await _auth.RegisterAsync(email, password, displayName);
        if (!ok) { ModelState.AddModelError(string.Empty, err!); return View(); }
        return RedirectToAction("Index","Users");
    }

    [HttpGet] public IActionResult Login(string? msg) { ViewBag.Msg = msg; return View(); }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string email, string password, bool rememberMe)
    {
        var (ok, err) = await _auth.LoginAsync(email, password, rememberMe);
        if (!ok) { ModelState.AddModelError(string.Empty, err!); return View(); }
        return RedirectToAction("Index","Users");
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _auth.LogoutAsync();
        return RedirectToAction(nameof(Login));
    }
}