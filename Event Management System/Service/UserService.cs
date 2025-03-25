// using Event_Management_System.DTO;
// using Event_Management_System.Enums;
// using Event_Management_System.Models;
// using Event_Management_System.Repository;
// using Microsoft.AspNetCore.Identity;
//
// namespace Event_Management_System.Service;
//
// public class UserService(IUserRepository userRepository) : IUserService
// {
//     public async Task<IdentityResult> RegisterAsync(UserDto userDto, string password)
//     {
//         var existingUser = await userRepository.FindByNameAsync(userDto.PassportId);
//         if (existingUser != null)
//         {
//             return IdentityResult.Failed(new IdentityError { Description = "User already exists." });
//         }
//         
//         var user = new User
//         {
//             PassportId = userDto.PassportId,
//             Name = userDto.Name
//         };
//         
//         return await userRepository.Create(user);
//     }
//
//     public async Task<IEnumerable<UserDto>> GetAllAsync()
//     {
//         var users = _userManager.Users.ToList();
//         return users.Select(user => new UserDto { PassportId = user.PassportId, Name = user.Name });
//     }
//
//     public async Task<UserDto?> GetByIdAsync(string passportId)
//     {
//         var user = await _userManager.FindByNameAsync(passportId);
//         return user != null ? new UserDto { PassportId = user.PassportId, Name = user.Name } : null;
//     }
//
//     public async Task<IdentityResult> UpdateAsync(UserDto userDto)
//     {
//         var user = await _userManager.FindByNameAsync(userDto.PassportId);
//         if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found." });
//         
//         user.Name = userDto.Name;
//         return await _userManager.UpdateAsync(user);
//     }
//
//     public async Task<IdentityResult> DeleteAsync(string passportId)
//     {
//         var user = await _userManager.FindByNameAsync(passportId);
//         if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found." });
//         return await _userManager.DeleteAsync(user);
//     }
// }
