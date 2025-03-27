using AutoMapper;
using Event_Management_System.DTO;
using Event_Management_System.Service;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(IUserService userService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{passportId}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var userEntity = await userService.GetByIdAsync(id);
        if (userEntity == null)
            return NotFound();
        
        return Ok(userEntity);
    }

    [HttpPut("{passportId}")]
    public async Task<IActionResult> UpdateUser(string passportId, [FromBody] UserDto userDto)
    {
        if (!ModelState.IsValid || passportId != userDto.PassportId)
            return BadRequest(ModelState);

        await userService.UpdateAsync(userDto);
        return NoContent();
    }

    [HttpDelete("{passportId}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var userEntity = await userService.GetByIdAsync(id);
        if (userEntity == null)
            return NotFound();
            
        await userService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetByName(string name)
    {
        var user = await userService.GetByName(name);
        return Ok(user);
        
    }
}
