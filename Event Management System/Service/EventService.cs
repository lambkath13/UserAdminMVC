using AutoMapper;
using Event_Management_System.DTO;
using Event_Management_System.Models;
using Event_Management_System.Repository;

namespace Event_Management_System.Service;

public class EventService:IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public EventService(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EventDto>> GetAllAsync()
    {
        var events = await _eventRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<EventDto>>(events);
    }

    public async Task<EventDto?> GetByIdAsync(int id)
    {
        var eventEntity = await _eventRepository.GetByIdAsync(id);
        return _mapper.Map<EventDto?>(eventEntity);
    }

    public async Task AddAsync(EventDto eventDto)
    {
        var eventEntity = _mapper.Map<Event>(eventDto);
        await _eventRepository.AddAsync(eventEntity); 
    }

    public async Task UpdateAsync(EventDto eventDto)
    {
        var eventEntity = _mapper.Map<Event>(eventDto);
        await _eventRepository.UpdateAsync(eventEntity);
    }

    public async Task DeleteAsync(int id)
    {
        await _eventRepository.DeleteAsync(id);
    }
}