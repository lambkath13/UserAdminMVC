using Event_Management_System.DTO;
using Event_Management_System.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers;

[Authorize]
public class SubscribeController(RegistrationService registrationService):Controller
{
    [HttpPost ("Subscribe")]
    public async Task<IActionResult> AddAsync([FromBody] EventRegistrationDto registrationDto)
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
    
    //Можно ли вообще так сделать?(логика только для кнопки этой)
    // [HttpPost("subscription")]
    // public async Task<IActionResult> Subscription([FromBody] EventRegistrationDto registrationDto)
    // {
    //     var existingSubscription = await registrationService.GetSubscriptionAsync(registrationDto.EventId, registrationDto.UserId);
    //
    //     if (existingSubscription != null)
    //     {
    //         await registrationService.RemoveSubscriptionAsync(existingSubscription);
    //         return Ok("Subscription cancelled.");
    //     }
    //     else
    //     {
    //         await registrationService.AddSubscriptionAsync(registrationDto);
    //         return Ok("Successfully subscribed.");
    //     }
    // }
    
}

