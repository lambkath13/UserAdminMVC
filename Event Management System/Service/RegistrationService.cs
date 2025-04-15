using AutoMapper;
using Event_Management_System.DTO;
using Event_Management_System.Models;
using Event_Management_System.Repository;

namespace Event_Management_System.Service;

public class RegistrationService: IRegistrationService
{
    private readonly IRegistrationRepository _registrationRepository;
    private readonly IMapper _mapper;

    public RegistrationService(IRegistrationRepository repository, IMapper mapper)
    {
        _registrationRepository = repository;
        _mapper = mapper;
    }

    public async Task AddAsync(EventRegistrationDto registrationDto)
    {
        var registrationEntity = _mapper.Map<EventRegistration>(registrationDto);
        await _registrationRepository.AddAsync(registrationEntity);
    }

    public async Task RemoveAsync(int eventId, Guid userId)
    {
        await _registrationRepository.RemoveAsync(eventId, userId);
    }
}