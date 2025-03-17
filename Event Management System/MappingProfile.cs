using AutoMapper;
using Event_Management_System.DTO;
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
        CreateMap<Post, PostDto>();
        CreateMap<CreatePostDto, Post>();

        // Post Comment
        CreateMap<PostComment, PostCommentDto>();

        // Image
        CreateMap<Image, ImageDto>();
    }
}