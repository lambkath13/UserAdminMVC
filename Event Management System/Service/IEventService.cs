using Event_Management_System.Dto.Event;
using Event_Management_System.Models;

namespace Event_Management_System.Service;

public interface IEventService
{
    Task<List<GetAllEventDto>> GetAllAsync(Guid? userId, string? query);
    Task<EventDto?> GetByIdAsync(int id);
    Task<int> AddAsync(CreateEventDto eventDto);
    Task UpdateAsync(UpdateEventDto eventDto);
    Task DeleteAsync(int id);
    Task CreateFeedback(CreateEventFeedbackDto model);
    Task<List<GetAllEventDto>> GetAllMyEvents(Guid? userId, string? query);
}
