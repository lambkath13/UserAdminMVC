using AutoMapper;
using Event_Management_System.Data;
using Event_Management_System.DTO;
using Event_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Repository;

public class EventRepository : IEventRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public EventRepository(AppDbContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EventDto>> GetAllAsync()
    {
        var events = await _context.Events.ToListAsync();
        return _mapper.Map<IEnumerable<EventDto>>(events);
    }

    public async Task<EventDto?> GetByIdAsync(int id)
    {
        var eventEntity = await _context.Events.FindAsync(id);
        return _mapper.Map<EventDto?>(eventEntity);
    }
    public async Task AddAsync(EventDto eventDto)
    {
        var eventEntity = _mapper.Map<Event>(eventDto);
        await _context.Events.AddAsync(eventEntity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(EventDto eventDto)
    {
        var eventEntity = _mapper.Map<Event>(eventDto);
        _context.Events.Update(eventEntity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var eventEntity = await _context.Events.FindAsync(id);
        if (eventEntity != null)
        {
            _context.Events.Remove(eventEntity);
            await _context.SaveChangesAsync();
        }
    }
}