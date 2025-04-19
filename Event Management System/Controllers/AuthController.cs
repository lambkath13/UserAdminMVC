using System.Security.Claims;
using AutoMapper;
using Event_Management_System.Dto.Auth;
using Event_Management_System.Enums;
using Event_Management_System.Models;
using Event_Management_System.Repository;
using Event_Management_System.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers;

public class AuthController(IUserService userService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : Controller
{
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new User()
        {
            Id = Guid.NewGuid(),
            PassportId = registerDto.PassportId,
            Name = registerDto.Name,
            Email = registerDto.Email,
            Role = UserRole.Student,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
        };

        await userService.AddAsync(user);
        
        await Authenticate(user.PassportId, user.Id, user.Role);
       
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var user = await userService.GetByPassportIdAsync(loginRequest.PassportId);
        if (user == null)
        {
            ModelState.AddModelError("", "Incorrect login or password!");
            return View(loginRequest);
        }
        
        if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
        {
            ModelState.AddModelError("", "Не правльный логин или пароль!");
            return View(loginRequest);
        }

        await Authenticate(user.PassportId, user.Id, user.Role);

        return RedirectToAction("Index", "Home");
    }
    
    private async Task Authenticate(string passportId, Guid userId, UserRole role)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, passportId),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Role, role.ToString())
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }
    
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        if (httpContextAccessor.HttpContext != null)
        {
            await httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        
        return RedirectToAction("Login");
    }
}
