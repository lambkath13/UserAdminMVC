using Event_Management_System.Dto.Event;
using Event_Management_System.Models;
using Event_Management_System.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers;

public class EventController(IEventService eventService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(string? query)
    {
        var userId = GetCurrentUserId();
        var events = await eventService.GetAllAsync(userId, query);
        var eventEntities = new GetEventEntityDto()
        {
            Entities = events,
            UserId = userId
        };
        return View(eventEntities);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllMyEvents(string? query)
    {
        var userId = GetCurrentUserId();
        var events = await eventService.GetAllMyEvents(userId, query);
        var eventEntities = new GetEventEntityDto()
        {
            Entities = events,
            UserId = userId
        };
        return View(eventEntities);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        var eventEntity = await eventService.GetByIdAsync(id);
        if (eventEntity == null)
            return NotFound();
        
        eventEntity.UserId = GetCurrentUserId();

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
        await eventService.AddAsync(eventDto);
        return Redirect("/event/getAll");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Update(int id)
    {
        var eventEntity = await eventService.GetByIdAsync(id);
        
        return View(new UpdateEventDto()
        {
            Description = eventEntity!.Description,
            EventDate = eventEntity.EventDate,
            Id = eventEntity.Id,
            Title = eventEntity.Title,
            Location = eventEntity.Location,
            Status = eventEntity.Status
        });
        
    }
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> UpdateEvent(UpdateEventDto eventDto)
    {
        await eventService.UpdateAsync(eventDto);
        return RedirectToAction("GetById", "Event", new { id = eventDto.Id });
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
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add(CreateEventFeedbackDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        model.UserId = GetCurrentUserId() ?? Guid.Empty;
        
        await eventService.CreateFeedback(model);

        return RedirectToAction("GetById", "Event", new { id = model.EventId });
    }
}
