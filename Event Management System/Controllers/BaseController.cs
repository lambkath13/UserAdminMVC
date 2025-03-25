using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers;

public class BaseController : Controller
{
    protected Guid GetCurrentUserId()
    {
        if (HttpContext.User.Identity is not ClaimsIdentity identity || !identity.IsAuthenticated)
        {
            throw new UnauthorizedAccessException("Пользователь не авторизован");
        }

        var userIdClaim = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
    
        if (string.IsNullOrEmpty(userIdClaim))
        {
            throw new UnauthorizedAccessException("Идентификатор пользователя отсутствует");
        }

        return Guid.Parse(userIdClaim);
    }

}