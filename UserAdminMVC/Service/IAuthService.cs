namespace UserAdminMVC.Service;
using System.Threading.Tasks;

public interface IAuthService
{
    Task<(bool ok, string? error)> RegisterAsync(string email, string password, string displayName);
    Task<(bool ok, string? error)> LoginAsync(string email, string password, bool remember);
    Task LogoutAsync();
}
