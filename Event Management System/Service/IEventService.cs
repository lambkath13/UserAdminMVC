using Event_Management_System.DTO;
using Event_Management_System.Models;

namespace Event_Management_System.Service;

public interface IEventService
{
    Task<List<GetAllEventDto>> GetAllAsync();
    Task<EventDto?> GetByIdAsync(int id);
    Task<int> AddAsync(CreateEventDto eventDto);
    Task UpdateAsync(EventDto eventDto, int id);
    Task DeleteAsync(int id);
}
