using Event_Management_System.DTO;
using Event_Management_System.Models;

namespace Event_Management_System.Repository;

public interface IEventRepository
{
    Task<IEnumerable<EventDto>> GetAllEventsAsync();
    Task<EventDto?> GetEventByIdAsync(int id);
    Task AddEventAsync(EventDto eventDto);
    Task UpdateEventAsync(EventDto eventDto);
    Task DeleteEventAsync(int id);
}