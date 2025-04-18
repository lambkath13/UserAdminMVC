using AutoMapper;
using Event_Management_System.Data;
using Event_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Repository;

public class EventRepository(AppDbContext context) : IEventRepository
{
    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        return await context.Events.ToListAsync();
    }

    public async Task<Event?> GetByIdAsync(int id)
    {
        return await context.Events.FindAsync(id);
    }
    public async Task<int> AddAsync(Event eventEntity)
    {
        await context.Events.AddAsync(eventEntity);
        await context.SaveChangesAsync();
        return eventEntity.Id;
    }

    public async Task UpdateAsync(Event eventEntity)
    {
        context.Events.Update(eventEntity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var eventEntity = await context.Events.FindAsync(id);
        if (eventEntity != null)
        {
            context.Events.Remove(eventEntity);
            await context.SaveChangesAsync();
        }
    }
}