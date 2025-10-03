using System.Security.Claims;
using UserAdminMVC.Models;

namespace UserAdminMVC.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserService
{
    Task<IReadOnlyList<ApplicationUser>> GetAllAsync();
    Task<(bool ok, string? error)> BlockAsync(IEnumerable<string> ids);
    Task<(bool ok, string? error)> UnblockAsync(IEnumerable<string> ids);
    Task<(bool ok, string? error)> DeleteAsync(IEnumerable<string> ids);
    Task<int> DeleteUnverifiedAsync(string? currentUserId = null);
    Task<bool> IsCurrentUserBlockedOrMissingAsync(ClaimsPrincipal principal);
}
