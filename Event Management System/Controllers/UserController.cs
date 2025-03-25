using AutoMapper;
using Event_Management_System.DTO;
using Event_Management_System.Service;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : BaseController
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
    }

    [HttpGet("{passportId}")]
    public async Task<IActionResult> GetUserById(string passportId)
    {
        var userEntity = await _userService.GetByIdAsync(passportId);
        if (userEntity == null)
            return NotFound();
        
        return Ok(_mapper.Map<UserDto>(userEntity));
    }

    [HttpPut("{passportId}")]
    public async Task<IActionResult> UpdateUser(string passportId, [FromBody] UserDto userDto)
    {
        if (!ModelState.IsValid || passportId != userDto.PassportId)
            return BadRequest(ModelState);

        var userEntity = _mapper.Map<UserDto>(userDto);
        await _userService.UpdateAsync(userEntity);
        return NoContent();
    }

    [HttpDelete("{passportId}")]
    public async Task<IActionResult> DeleteUser(string passportId)
    {
        var userEntity = await _userService.GetByIdAsync(passportId);
        if (userEntity == null)
            return NotFound();
            
        await _userService.DeleteAsync(passportId);
        return NoContent();
    }
}
