using AutoMapper;
using Event_Management_System.DTO;
using Event_Management_System.Models;
using Event_Management_System.Repository;
using Microsoft.AspNetCore.Identity;

namespace Event_Management_System.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IdentityResult> AddAsync(UserDto userDto, string password)
    {
        var userEntity = _mapper.Map<User>(userDto);
        return await _userRepository.AddAsync(userEntity, password);
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto?> GetByIdAsync(Guid id)
    {
        var userEntity = await _userRepository.GetByIdAsync(id);
        return _mapper.Map<UserDto?>(userEntity);
    }

    public async Task<IdentityResult> UpdateAsync(UserDto userDto)
    {
        var userEntity = _mapper.Map<User>(userDto);
        return await _userRepository.UpdateAsync(userEntity);
    }

    public async Task<IdentityResult> DeleteAsync(Guid id)
    {
        return await _userRepository.DeleteAsync(id);
    }

    public async Task<UserDto> GetByPassportIdAsync(string loginRequestPassportId)
    {
        var user = await _userRepository.GetByPassportId(loginRequestPassportId);
        return _mapper.Map<UserDto>(user);
    }
}
