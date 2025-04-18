using AutoMapper;
using Event_Management_System.DTO;
using Event_Management_System.Models;
using Event_Management_System.Repository;

namespace Event_Management_System.Service;

public class RegistrationService(IRegistrationRepository registrationRepository) : IRegistrationService
{
    public async Task AddAsync(EventRegistrationDto registrationDto)
    {
        var registration = await registrationRepository.GetByUserIdAndEventId(registrationDto.UserId, registrationDto.EventId);
        if (registration != null)
            await RemoveAsync(registration.EventId, registration.UserId);
        else
        {
            var registrationEntity = new EventRegistration()
            {
                EventId = registrationDto.EventId,
                UserId = registrationDto.UserId,
                RegisteredAt = DateTime.Now
            };
            await registrationRepository.AddAsync(registrationEntity);
        }
    }

    public async Task RemoveAsync(int eventId, Guid userId)
    {
        var registration = await registrationRepository.GetByUserIdAndEventId(userId, eventId);
        if (registration != null)
            await registrationRepository.RemoveAsync(registration);
    }
}