using Event_Management_System.Models;

namespace Event_Management_System.Repository;

public interface IRegistrationRepository
{
    Task AddAsync(EventRegistration registration);
    Task RemoveAsync(EventRegistration registration);
    bool GetByEventId(int argId);
    Task<EventRegistration?> GetByUserIdAndEventId(Guid registrationDtoUserId, int registrationDtoEventId);
}