using AutoMapper;
using Event_Management_System.DTO;
using Event_Management_System.Service;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers;

[ApiController]
[Route("api/events")]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly IMapper _mapper;

    public EventController(IEventService eventService, IMapper mapper)
    {
        _eventService = eventService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        var events = await _eventService.GetAllEventsAsync();
        return Ok(_mapper.Map<IEnumerable<EventDto>>(events));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(int id)
    {
        var eventEntity = await _eventService.GetEventByIdAsync(id);
        if (eventEntity == null)
            return NotFound();

        return Ok(_mapper.Map<EventDto>(eventEntity));
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto createEventDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var eventEntity = _mapper.Map<EventDto>(createEventDto);
        await _eventService.AddEventAsync(eventEntity);

        return CreatedAtAction(nameof(GetEventById), new { id = eventEntity.EventId }, _mapper.Map<EventDto>(eventEntity));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventDto eventDto)
    {
        if (!ModelState.IsValid || id != eventDto.EventId)
            return BadRequest(ModelState);

        var eventEntity = _mapper.Map<EventDto>(eventDto);
        await _eventService.UpdateEventAsync(eventEntity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var eventExists = await _eventService.GetEventByIdAsync(id);
        if (eventExists == null)
            return NotFound();

        await _eventService.DeleteEventAsync(id);
        return NoContent();
    }
}
