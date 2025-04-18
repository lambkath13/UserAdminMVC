using AutoMapper;
using Event_Management_System.Dto.Event;
using Event_Management_System.Dto.Post;
using Event_Management_System.Dto.User;
using Event_Management_System.Models;

namespace Event_Management_System;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User
        CreateMap<User, UserDto>();

        // Event
        CreateMap<Event, EventDto>();
        CreateMap<CreateEventDto, Event>();

        // Event Feedback
        CreateMap<EventFeedback, EventFeedbackDto>();
        
        // Event Registration
        CreateMap<EventRegistration, EventRegistrationDto>();

        // Post
        CreateMap<PostDto, Post>();
        CreateMap<Post, PostDto>();

        // Post Comment
        CreateMap<PostComment, PostCommentDto>();
    }
}