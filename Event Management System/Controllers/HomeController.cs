using AutoMapper;
using Event_Management_System.Dto.Event;
using Event_Management_System.Service;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers;

public class HomeController(IEventService eventService) : BaseController
{
    public async  Task<IActionResult> Index(string? query, int pageNumber = 1, int pageSize = 12)
    {
        var userId = GetCurrentUserId();
        var events = await eventService.GetAllAsync(userId, query, pageNumber, pageSize);

        var model = new GetEventEntityDto
        {
            Entities = events.Item1,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = events.Item2,
            UserId = GetCurrentUserId(), // если нужно
        };

        return View(model);
    }
}