using AutoMapper;
using Event_Management_System.Dto.Event;
using Event_Management_System.Models;
using Event_Management_System.Repository;

namespace Event_Management_System.Service;

public class EventService(IEventRepository eventRepository, IMapper mapper, IImageService imageService, IRegistrationRepository registrationRepository) : IEventService
{
    public async Task<List<GetAllEventDto>> GetAllAsync()
    {
        var events = await eventRepository.GetAllAsync();
        return events.Select(x=> new GetAllEventDto()
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            Location = x.Location,
            EventDate = x.EventDate,
            Status = x.Status,
            OrganizerId = x.UserId,
            File = x.File,
            IsSubscribe = registrationRepository.GetByEventId(x.Id)
        }).ToList();
    }

    public async Task<EventDto?> GetByIdAsync(int id)
    {
        var eventEntity = await eventRepository.GetByIdAsync(id);
        return mapper.Map<EventDto?>(eventEntity);
    }

    public async Task<int> AddAsync(CreateEventDto eventDto)
    {
        var eventEntity = mapper.Map<Event>(eventDto);
        eventEntity.UserId = eventDto.OrganizerId ?? eventEntity.UserId;
        eventEntity.File = await imageService.AddFileAsync(nameof(Event),eventDto.File);
        return await eventRepository.AddAsync(eventEntity); 
    }

    public async Task UpdateAsync(EventDto eventDto, int id)
    {
        var eventE = await eventRepository.GetByIdAsync(id);
        if (eventE == null)
            return;
        
        eventE.Description = eventDto.Description;
        eventE.EventDate = eventDto.EventDate;
        eventE.Location = eventDto.Location;
        eventE.UserId = eventDto.OrganizerId ?? eventE.UserId;
        eventE.Title = eventDto.Title;
        eventE.Status = eventDto.Status;
        
        await eventRepository.UpdateAsync(eventE);
    }

    public async Task DeleteAsync(int id)
    {
        await eventRepository.DeleteAsync(id);
    }
}