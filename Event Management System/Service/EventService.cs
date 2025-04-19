using AutoMapper;
using Event_Management_System.Dto.Event;
using Event_Management_System.Models;
using Event_Management_System.Repository;

namespace Event_Management_System.Service;

public class EventService(IEventRepository eventRepository, IMapper mapper, IImageService imageService, IRegistrationRepository registrationRepository, INotificationRepository notificationRepository) : IEventService
{
    public async Task<(List<GetAllEventDto>, int)> GetAllAsync(Guid? userId, string? query, int pageNumber, int pageSize)
    {
        var (entities, totalCount) = await eventRepository.GetAllAsync(userId, query, pageNumber, pageSize);

        var result = entities.Select(x => new GetAllEventDto
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            Location = x.Location,
            EventDate = x.EventDate,
            Status = x.Status,
            OrganizerId = x.UserId,
            File = x.File,
            IsSubscribe = registrationRepository.GetByEventId(x.Id),
            Rating = CalculateAverageRating(x.EventFeedbacks)
        }).ToList();

        return (result, totalCount);
    }
    
    private double CalculateAverageRating(List<EventFeedback>? feedbacks)
    {
        if (feedbacks == null || !feedbacks.Any())
            return 0;

        return Math.Ceiling(feedbacks.Average(f => f.Rating));
    }

    public async Task<EventDto?> GetByIdAsync(int id)
    {
        var eventEntity = await eventRepository.GetByIdAsync(id);
        if (eventEntity == null)
            return null;
       
        return new EventDto()
        {
            Id = eventEntity.Id,
            Title = eventEntity.Title,
            Description = eventEntity.Description,
            Location = eventEntity.Location,
            EventDate = eventEntity.EventDate,
            Status = eventEntity.Status,
            OrganizerId = eventEntity.UserId,
            File = eventEntity.File,
            Users = eventEntity.EventRegistrations.Select(x => new EventUserDto()
            {
                Id = x.UserId,
                FullName = x.User.Name,
                RegisteredAt = x.RegisteredAt
            }).ToList(),
            Feedbacks = eventEntity.EventFeedbacks.Select(x => new EventFeedbackDto()
            {
                Content = x.Content,
                Rating = x.Rating,
                CreatedAt = x.CreatedAt,
                User = new GetEventFeedbackUserDto()
                {
                    Id = x.UserId,
                    FullName = x.User.Name
                }
            }).ToList()
        };
    }

    public async Task<int> AddAsync(CreateEventDto eventDto)
    {
        var eventEntity = mapper.Map<Event>(eventDto);
        eventEntity.UserId = eventDto.OrganizerId ?? eventEntity.UserId;
        eventEntity.File = await imageService.AddFileAsync(nameof(Event),eventDto.File);

        var eventId = await eventRepository.AddAsync(eventEntity); 
        
        await notificationRepository.AddAsync(new Notification()
        {
            Description = $"A new event \"{eventEntity.Title}\" has just been added. Check it out!",
            Title = "Exciting News! New Event Available",
            CreatedAt = DateTime.Now,
            IsNew = true,
            Link = $"/event/getById/{eventId}"
        });
        return eventId;
    }

    public async Task UpdateAsync(UpdateEventDto eventDto)
    {
        var eventE = await eventRepository.GetByIdAsync(eventDto.Id);
        if (eventE == null)
            return;
        
        eventE.Description = eventDto.Description;
        eventE.EventDate = eventDto.EventDate;
        eventE.Location = eventDto.Location;
        eventE.Title = eventDto.Title;
        eventE.Status = eventDto.Status;

        if (eventDto.File != null)
        {
            eventE.File = await imageService.AddFileAsync(nameof(Event),eventDto.File);
        }
        
        await eventRepository.UpdateAsync(eventE);
    }

    public async Task DeleteAsync(int id)
    {
        await eventRepository.DeleteAsync(id);
    }

    public async Task CreateFeedback(CreateEventFeedbackDto model)
    {
        var eventFeedback = new EventFeedback()
        {
            EventId = model.EventId,
            UserId = model.UserId ?? Guid.Empty,
            Content = model.Content,
            Rating = model.Rating,
            CreatedAt = DateTime.Now
        };
        await eventRepository.CreateFeedback(eventFeedback);
    }

    public async Task<List<GetAllEventDto>> GetAllMyEvents(Guid? userId, string? query)
    {
        var events = await eventRepository.GetAllMyEvents(userId, query);
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
            IsSubscribe = registrationRepository.GetByEventId(x.Id),
            Rating = CalculateAverageRating(x.EventFeedbacks)
        }).ToList();
    }
}