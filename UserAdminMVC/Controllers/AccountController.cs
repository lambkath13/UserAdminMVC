using Microsoft.AspNetCore.Mvc;
using UserAdminMVC.Service;

namespace UserAdminMVC.Controllers;

public class AccountController : Controller
{
    private readonly IAuthService _auth;

    // note: Auth service handles all login/register logic and database communication
    public AccountController(IAuthService auth) => _auth = auth;

    // important: registration form view (GET)
    [HttpGet] 
    public IActionResult Register() => View();

    // nota bene: registration submits form data and passes to AuthService
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(string email, string password, string displayName)
    {
        var (ok, err) = await _auth.RegisterAsync(email, password, displayName);
        if (!ok) 
        { 
            // note: show validation error if password or email invalid
            ModelState.AddModelError(string.Empty, err!); 
            return View(); 
        }

        // important: redirect to Users list if registration succeeded
        return RedirectToAction("Index", "Users");
    }

    // note: login form view (GET)
    [HttpGet] 
    public IActionResult Login(string? msg) 
    { 
        ViewBag.Msg = msg; 
        return View(); 
    }

    // nota bene: verifies user credentials and creates authentication cookie
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string email, string password, bool rememberMe)
    {
        var (ok, err) = await _auth.LoginAsync(email, password, rememberMe);
        if (!ok) 
        { 
            // important: invalid credentials or blocked user
            ModelState.AddModelError(string.Empty, err!); 
            return View(); 
        }

        // note: redirect to main Users page after successful login
        return RedirectToAction("Index", "Users");
    }

    // important: sign-out endpoint clears user session and redirects to login
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _auth.LogoutAsync();
        return RedirectToAction(nameof(Login));
    }
}
