using Event_Management_System.Dto.User;
using Event_Management_System.Models;
using Event_Management_System.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers;

public class UserController(IUserService userService, IImageService imageService) : BaseController
{
    
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateUserDto userDto)
    {
        if (!ModelState.IsValid)
            return View(userDto);
        
        var statusCode = await userService.AddAsync(new User
        {
            PassportId = userDto.PassportId,
            Name = userDto.Name,
            Email = userDto.Email,
            Role = userDto.Role,
            AvatarUrl = userDto.AvatarUrl != null
                ? await imageService.AddFileAsync((nameof(User)), userDto.AvatarUrl)
                : null,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
            IsFirstAdmin = false
        });
       

        if (statusCode == 201)
            return RedirectToAction("GetAll", "User");

        if (statusCode == 402)
        {
            ModelState.AddModelError(string.Empty, "A user with this email already exists.");
            return View(userDto);
        } 
        if (statusCode == 403)
        {
            ModelState.AddModelError(string.Empty, "A user with this passportId already exists.");
            return View(userDto);
        }

        ModelState.AddModelError(string.Empty, "Unexpected error occurred.");
        return View(userDto);
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> GetAll(string? query, int pageNumber = 1, int pageSize = 12)
    {
        var users = await userService.GetAllAsync(query, pageNumber, pageSize);
        
        var model = new GetUserDto()
        {
            Entities = users.Item1,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = users.Item2,
            UserId = GetCurrentUserId(),
        };

        return View(model);
    }
    
    [HttpGet]
    [Authorize]

    public async Task<IActionResult> Me()
    {
        var userId = GetCurrentUserId();
        if (userId == null)
            return Redirect("/login");
       
        var userEntity = await userService.GetByIdAsync(userId);
       
        if (userEntity == null)
            return NotFound();
        
        return View(userEntity);
    }

    [HttpGet("{passportId}")]
    [Authorize]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var userEntity = await userService.GetByIdAsync(id);
        if (userEntity == null)
            return NotFound();
        
        return View(userEntity);
    }

    [HttpPut("{passportId}")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(string passportId, [FromBody] UserDto userDto)
    {
        if (!ModelState.IsValid || passportId != userDto.PassportId)
            return BadRequest(ModelState);

        await userService.UpdateAsync(userDto);
        return NoContent();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await userService.DeleteAsync(id);
        return RedirectToAction("GetAll", "User");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetByName(string name)
    {
        var user = await userService.GetByName(name);
        return Ok(user);
        
    }
}
