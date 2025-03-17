using Event_Management_System.DTO;
using Event_Management_System.Repository;

namespace Event_Management_System.Service;

public class EventService:IEventService
{
    private readonly IEventRepository _eventRepository;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
    {
        return await _eventRepository.GetAllEventsAsync();
    }

    public async Task<EventDto?> GetEventByIdAsync(int id)
    {
        return await _eventRepository.GetEventByIdAsync(id);
    }

    public async Task AddEventAsync(EventDto eventDto)
    {
        await _eventRepository.AddEventAsync(eventDto);
    }

    public async Task UpdateEventAsync(EventDto eventDto)
    {
        await _eventRepository.UpdateEventAsync(eventDto);
    }

    public async Task DeleteEventAsync(int id)
    {
        await _eventRepository.DeleteEventAsync(id);
    }
}