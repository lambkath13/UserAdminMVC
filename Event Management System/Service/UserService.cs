using Event_Management_System.DTO;
using Event_Management_System.Repository;
using Microsoft.AspNetCore.Identity;

namespace Event_Management_System.Service;

public class UserService(IUserRepository userRepository) : IUserService
{
    public Task<IdentityResult> Create(UserDto userDto, string password)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserDto?> GetByIdAsync(string passportId)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> UpdateAsync(UserDto userDto)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> DeleteAsync(string passportId)
    {
        throw new NotImplementedException();
    }
}
