using AutoMapper;
using Event_Management_System.DTO;
using Event_Management_System.Enums;
using Event_Management_System.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers;

public class AuthController(IUserRepository userRepository, IMapper mapper) : Controller
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
            Role = registerDto.Role
        };

        var result = await userRepository.AddAsync(user, registerDto.Password);
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
        var user = await userRepository.AuthenticateAsync(loginRequest.PassportId, loginRequest.Password);
        if (user == null) return Unauthorized("Invalid credentials");
        return Ok(new { message = "Login successful", user });
    }
}
