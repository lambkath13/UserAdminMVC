using Event_Management_System.DTO;
using Event_Management_System.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers;

[Authorize]
public class SubscribeController(RegistrationService registrationService):Controller
{
    [HttpPost ("Subscribe")]
    public async Task<IActionResult> AddAsync(EventRegistrationDto registrationDto)
    {
        await registrationService.AddAsync(registrationDto);
        return Ok("Event registration successfully added");
    }

    [HttpDelete("Unsubscribe")]
    public async Task<IActionResult> UnsubscribeAsync(int eventId, Guid userId)
    {
        await registrationService.RemoveAsync(eventId, userId);
        return Ok("Event registration successfully removed");
    }
    
}