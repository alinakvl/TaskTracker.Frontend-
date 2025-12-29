using TaskTracker.Blazor.Domain.DTOs.Users;

namespace TaskTracker.Blazor.Services.Abstraction;

public interface IUserService
{
    Task<List<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(Guid id);
    Task<UserDto?> GetUserByEmailAsync(string email);
    Task<UserDto?> UpdateUserAsync(Guid id, UpdateUserDto updateUserDto);
    Task<bool> DeleteUserAsync(Guid id);
    //Task<bool> ChangeUserRoleAsync(Guid userId, string role);
    Task<bool> ChangeUserRoleAsync(Guid userId, ChangeRoleDto roleDto);
}