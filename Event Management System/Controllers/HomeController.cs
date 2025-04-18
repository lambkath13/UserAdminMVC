using AutoMapper;
using Event_Management_System.Dto.Event;
using Event_Management_System.Service;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers;

public class HomeController(IEventService eventService) : BaseController
{
    public async  Task<IActionResult> Index(string? query)
    {
        var userId = GetCurrentUserId();
        var events = await eventService.GetAllAsync(userId, query);
        var eventEntities = new GetEventEntityDto()
        {
            Entities = events,
            UserId = userId
        };
        return View(eventEntities);
    }
}