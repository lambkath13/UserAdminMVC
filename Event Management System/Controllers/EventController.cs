using AutoMapper;
using Event_Management_System.DTO;
using Event_Management_System.Service;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers;

public class EventController(IEventService eventService, IMapper mapper) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        var events = await eventService.GetAllAsync();
        return Ok(mapper.Map<IEnumerable<EventDto>>(events));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(int id)
    {
        var eventEntity = await eventService.GetByIdAsync(id);
        if (eventEntity == null)
            return NotFound();

        return Ok(mapper.Map<EventDto>(eventEntity));
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEventDto createEventDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var eventEntity = mapper.Map<EventDto>(createEventDto);
        await eventService.AddAsync(eventEntity);

        return CreatedAtAction(nameof(GetEventById), new { id = eventEntity.EventId }, mapper.Map<EventDto>(eventEntity));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventDto eventDto)
    {
        if (!ModelState.IsValid || id != eventDto.EventId)
            return BadRequest(ModelState);

        var eventEntity = mapper.Map<EventDto>(eventDto);
        await eventService.UpdateAsync(eventEntity);
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
