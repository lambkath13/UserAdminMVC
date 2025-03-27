using System.Security.Claims;
using AutoMapper;
using Event_Management_System.DTO;
using Event_Management_System.Enums;
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
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (registerDto.Role == UserRole.Admin && registerDto.AdminPassword != "Special_Admin_Password")
        {
            return BadRequest("Invalid admin password");
        }

        var user = new UserDto
        {
            PassportId = registerDto.PassportId,
            Name = registerDto.Name,
            Email = registerDto.Email,
            Role = registerDto.Role,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
        };

        var result = await userService.AddAsync(user);
        return result.Succeeded ? Ok("User registered successfully") : BadRequest(result.Errors);
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

        await Authenticate(user.PassportId, user.Id);

        return RedirectToAction("Index", "Home");
    }
    
    private async Task Authenticate(string passportId, Guid userId)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, passportId),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
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
