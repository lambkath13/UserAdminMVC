using Event_Management_System.Models;

namespace Event_Management_System.Repository;

public interface IRegistrationRepository
{
    
    Task AddAsync(EventRegistration registration);
    Task RemoveAsync(int eventId, Guid userId);
}