using Event_Management_System.Models;

namespace Event_Management_System.Repository;

public interface IEventRepository
{
    Task<(List<Event>, int)> GetAllAsync(Guid? userId, string? query, int pageNumber, int pageSize);
    Task<Event?> GetByIdAsync(int id);
    Task<int> AddAsync(Event eventEntity);
    Task UpdateAsync(Event eventEntity);
    Task DeleteAsync(int id);
    Task CreateFeedback(EventFeedback model);
    Task<IEnumerable<Event>> GetAllMyEvents(Guid? userId, string? query);
}