using Event_Management_System.Dto.Event;
using Event_Management_System.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers;

public class EventController(IEventService eventService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = GetCurrentUserId();
        var events = await eventService.GetAllAsync();
        var eventEntities = new GetEventEntityDto()
        {
            Entities = events,
            UserId = userId
        };
        return View(eventEntities);
    }

    public async Task<IActionResult> GetById(int id)
    {
        var eventEntity = await eventService.GetByIdAsync(id);
        if (eventEntity == null)
            return NotFound();

        return View(eventEntity);
    }

    [HttpGet]
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CreateEventDto eventDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = GetCurrentUserId();
        eventDto.OrganizerId = userId;
        var eventId = await eventService.AddAsync(eventDto);
        return Redirect("/event/getAll");
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateEvent(int id,  EventDto eventDto)
    {
        await eventService.UpdateAsync(eventDto, id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var eventExists = await eventService.GetByIdAsync(id);
        if (eventExists == null)
            return NotFound();

        await eventService.DeleteAsync(id);
        return NoContent();
    }
}
