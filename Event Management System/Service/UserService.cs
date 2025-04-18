using AutoMapper;
using Event_Management_System.DTO;
using Event_Management_System.Models;
using Event_Management_System.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Service;

public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
{
    public async Task<IdentityResult> AddAsync(User user)
    {
        return await userRepository.AddAsync(user);
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await userRepository.GetAllAsync();
        return mapper.Map<IEnumerable<UserDto>>(users);
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

    public async Task<UserDto> GetByPassportIdAsync(string loginRequestPassportId)
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
