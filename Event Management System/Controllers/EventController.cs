using AutoMapper;
using Event_Management_System.DTO;
using Event_Management_System.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers;

public class EventController(IEventService eventService) : Controller
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllEvents()
    {
        var events = await eventService.GetAllAsync();
        return Ok(events);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(int id)
    {
        var eventEntity = await eventService.GetByIdAsync(id);
        if (eventEntity == null)
            return NotFound();

        return Ok(eventEntity);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EventDto eventDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await eventService.AddAsync(eventDto);

        return CreatedAtAction(nameof(GetEventById), new { id = eventDto.EventId }, eventDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventDto eventDto)
    {
        if (!ModelState.IsValid || id != eventDto.EventId)
            return BadRequest(ModelState);

        await eventService.UpdateAsync(eventDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var eventExists = await eventService.GetByIdAsync(id);
        if (eventExists == null)
            return NotFound();

        await eventService.DeleteAsync(id);
        return NoContent();
    }
}
