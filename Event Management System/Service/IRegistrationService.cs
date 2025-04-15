using Event_Management_System.DTO;

namespace Event_Management_System.Service;

public interface IRegistrationService
{
    Task AddAsync(EventRegistrationDto registration);
    Task RemoveAsync(int eventId, Guid userId);
}