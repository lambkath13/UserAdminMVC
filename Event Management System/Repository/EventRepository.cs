using AutoMapper;
using Event_Management_System.Data;
using Event_Management_System.DTO;
using Event_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Repository;

public class EventRepository : IEventRepository
{
    private readonly AppDbContext _context;

    public EventRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        return await _context.Events.ToListAsync();
    }

    public async Task<Event?> GetByIdAsync(int id)
    {
        return await _context.Events.FindAsync(id);
    }
    public async Task AddAsync(Event eventEntity)
    {
        await _context.Events.AddAsync(eventEntity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Event eventEntity)
    {
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