using AutoMapper;
using Event_Management_System.DTO;
using Event_Management_System.Enums;
using Event_Management_System.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public AuthController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    [HttpPost("register")]
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

        var result = await _userRepository.AddUserAsync(user, registerDto.Password);
        return result.Succeeded ? Ok("User registered successfully") : BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] string passportId, string password)
    {
        var user = await _userRepository.AuthenticateAsync(passportId, password);
        if (user == null) return Unauthorized("Invalid credentials");
        return Ok(new { message = "Login successful", user });
    }
}
