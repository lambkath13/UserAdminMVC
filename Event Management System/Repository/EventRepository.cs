using AutoMapper;
using Event_Management_System.Data;
using Event_Management_System.Enums;
using Event_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Repository;

public class EventRepository(AppDbContext context) : IEventRepository
{
    public async Task<(List<Event>, int)> GetAllAsync(Guid? userId, string? query, int pageNumber, int pageSize)
    {
        var events = context.Events
            .Include(x => x.EventFeedbacks)
            .Where(x => 
                (userId == null && x.Status != EventStatus.Completed) ||
                (userId != null && x.Status != EventStatus.Completed) ||
                (userId != null && x.UserId == userId)
            );

        if (!string.IsNullOrWhiteSpace(query))
        {
            var loweredQuery = query.ToLower();
            events = events.Where(x =>
                x.Title.ToLower().Contains(loweredQuery)
            );
        }
        
        var pagedEvents = await events
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (pagedEvents, await events.CountAsync());
    }

    public async Task<Event?> GetByIdAsync(int id)
    {
        return await context.Events
            .Include(x=>x.EventFeedbacks)
            .ThenInclude(x=>x.User)
            .Include(x=>x.EventRegistrations)
            .ThenInclude(x=>x.User)
            .Include(x=>x.Posts)
            .ThenInclude(x=>x.User)
            .FirstOrDefaultAsync(x=>x.Id == id);
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

    public async Task CreateFeedback(EventFeedback model)
    {
        await context.EventFeedbacks.AddAsync(model);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Event>> GetAllMyEvents(Guid? userId, string? query)
    {
        
        var events = context.Events
            .Include(x => x.EventFeedbacks)
            .Where(x=> x.EventRegistrations.Any(r=> r.UserId == userId));

        if (!string.IsNullOrWhiteSpace(query))
        {
            var loweredQuery = query.ToLower();
            events = events.Where(x =>
                x.Title.ToLower().Contains(loweredQuery)
            );
        }

        return await events.ToListAsync();
    }
}