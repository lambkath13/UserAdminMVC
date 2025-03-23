using Event_Management_System.DTO;
using Event_Management_System.Models;

namespace Event_Management_System.Service;

public interface IEventService
{
    Task<IEnumerable<EventDto>> GetAllAsync();
    Task<EventDto?> GetByIdAsync(int id);
    Task AddAsync(EventDto eventDto);
    Task UpdateAsync(EventDto eventDto);
    Task DeleteAsync(int id);
}
