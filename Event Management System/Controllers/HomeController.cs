using AutoMapper;
using Event_Management_System.DTO;
using Event_Management_System.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers;

[Authorize]
public class HomeController(IEventService eventService, IMapper mapper) : Controller
{
    public async  Task<IActionResult> Index()
    {
        var events = await eventService.GetAllAsync();
        return View(mapper.Map<List<EventDto>>(events));
    }
}