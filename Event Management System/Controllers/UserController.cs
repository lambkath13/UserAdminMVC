using AutoMapper;
using Event_Management_System.DTO;
using Event_Management_System.Service;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(IUserService userService, IMapper mapper) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await userService.GetAllAsync();
        return Ok(mapper.Map<IEnumerable<UserDto>>(users));
    }

    [HttpGet("{passportId}")]
    public async Task<IActionResult> GetUserById(string passportId)
    {
        var userEntity = await userService.GetByIdAsync(passportId);
        if (userEntity == null)
            return NotFound();
        
        return Ok(mapper.Map<UserDto>(userEntity));
    }

    [HttpPut("{passportId}")]
    public async Task<IActionResult> UpdateUser(string passportId, [FromBody] UserDto userDto)
    {
        if (!ModelState.IsValid || passportId != userDto.PassportId)
            return BadRequest(ModelState);

        var userEntity = mapper.Map<UserDto>(userDto);
        await userService.UpdateAsync(userEntity);
        return NoContent();
    }

    [HttpDelete("{passportId}")]
    public async Task<IActionResult> DeleteUser(string passportId)
    {
        var userEntity = await userService.GetByIdAsync(passportId);
        if (userEntity == null)
            return NotFound();
            
        await userService.DeleteAsync(passportId);
        return NoContent();
    }
}
