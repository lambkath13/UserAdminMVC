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

    public async Task<IEnumerable<EventDto>> GetAllAsync()
    {
        return await _eventRepository.GetAllAsync();
    }

    public async Task<EventDto?> GetByIdAsync(int id)
    {
        return await _eventRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(EventDto eventDto)
    {
        await _eventRepository.AddAsync(eventDto);
    }

    public async Task UpdateAsync(EventDto eventDto)
    {
        await _eventRepository.UpdateAsync(eventDto);
    }

    public async Task DeleteAsync(int id)
    {
        await _eventRepository.DeleteAsync(id);
    }
}