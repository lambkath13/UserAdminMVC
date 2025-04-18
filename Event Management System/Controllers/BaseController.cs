using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers;

public class BaseController : Controller
{
    protected Guid? GetCurrentUserId()
    {
        if (HttpContext.User.Identity is not ClaimsIdentity identity || !identity.IsAuthenticated)
        {
            return null;
        }

        var userIdClaim = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            return null;
        }

        if (Guid.TryParse(userIdClaim, out var userId))
        {
            return userId;
        }

        return null;
    }
}
