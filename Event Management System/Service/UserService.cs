using AutoMapper;
using Event_Management_System.Dto.User;
using Event_Management_System.Models;
using Event_Management_System.Repository;
using Microsoft.AspNetCore.Identity;

namespace Event_Management_System.Service;

public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
{
    public async Task<int> AddAsync(User user)
    { 
        var entity = await userRepository.GetByPassportId(user.PassportId);
        if (entity != null)
            return 403;
        var entity2 = await userRepository.GetByEmail(user.Email);
        if (entity2 != null)
            return 402;
        
        await userRepository.AddAsync(user);
        
        return 201;
    }

    public async Task<(List<UserDto>, int)> GetAllAsync(string? query, int pageNumber = 1, int pageSize = 12)
    {
        var users = await userRepository.GetAllAsync(query, pageNumber, pageSize);
        return (users.Item1.Select(x=> new UserDto()
        {
            Id = x.Id,
            Name = x.Name,
            Email = x.Email,
            AvatarUrl = x.AvatarUrl,
            PassportId = x.PassportId,
            PasswordHash = x.PasswordHash,
            Role = x.Role,
            IsFirstAdmin = x.IsFirstAdmin
        }).ToList(), users.Item2);
    }

    public async Task<UserDto?> GetByIdAsync(Guid? id)
    {
        var userEntity = await userRepository.GetByIdAsync(id);
        return mapper.Map<UserDto?>(userEntity);
    }

    public async Task<IdentityResult> UpdateAsync(UserDto userDto)
    {
        var userEntity = mapper.Map<User>(userDto);
        return await userRepository.UpdateAsync(userEntity);
    }

    public async Task<IdentityResult> DeleteAsync(Guid id)
    {
        var userEntity = await userRepository.GetByIdAsync(id);
        if (userEntity == null)
            return IdentityResult.Failed();
        
        return await userRepository.DeleteAsync(userEntity);
    }

    public async Task<UserDto?> GetByPassportIdAsync(string loginRequestPassportId)
    {
        var user = await userRepository.GetByPassportId(loginRequestPassportId);
        return mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetByName(string name)
    {
        var user = await userRepository.GetByName(name);
        return new UserDto()
        {
            Id = user.Id,
            Name = user.Name,
            AvatarUrl = user.AvatarUrl
        };
    }
}
