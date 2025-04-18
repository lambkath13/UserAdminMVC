using Event_Management_System.Dto.Event;
using Event_Management_System.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers;

[Authorize]
public class SubscribeController(IRegistrationService registrationService) : BaseController
{
    [HttpGet("/event/subscribe/{id:int}/user/{userId:guid}")]
    public async Task<IActionResult> AddAsync(int id, Guid userId)
    {
        var registrationDto = new EventRegistrationDto()
        {
            EventId = id,
            UserId = userId
        };
        await registrationService.AddAsync(registrationDto);
        return Redirect("/event/getAll");
    }

    [HttpDelete("Unsubscribe")]
    public async Task<IActionResult> RemoveAsync(int eventId, Guid userId)
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

