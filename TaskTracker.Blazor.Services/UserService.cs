using TaskTracker.Blazor.Domain.DTOs.Users;
using TaskTracker.Blazor.Services.Abstraction;
using TaskTracker.Blazor.Services.Abstraction.ExternalApi;

namespace TaskTracker.Blazor.Services;

public class UserService : IUserService
{
    private readonly IUserApi _userApi;

    public UserService(IUserApi userApi)
    {
        _userApi = userApi;
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var response = await _userApi.GetAllUsersAsync();

        if (response.IsSuccessStatusCode && response.Content != null)
        {
            return response.Content;
        }

        return new List<UserDto>();
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        var response = await _userApi.GetUserByIdAsync(id);

        if (response.IsSuccessStatusCode)
        {
            return response.Content;
        }

        return null;
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        var users = await GetAllUsersAsync();

        return users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<UserDto?> UpdateUserAsync(Guid id, UpdateUserDto updateUserDto)
    {
        var response = await _userApi.UpdateUserAsync(id, updateUserDto);

        if (response.IsSuccessStatusCode)
        {
            return response.Content;
        }

        return null;
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var response = await _userApi.DeleteUserAsync(id);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ChangeUserRoleAsync(Guid userId, ChangeRoleDto roleDto)
    {
        var response = await _userApi.ChangeUserRoleAsync(userId, roleDto);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<UserDto>> SearchUsersAsync(string term)
    {
        var response = await _userApi.SearchUsersAsync(term);

        if (response.IsSuccessStatusCode && response.Content != null)
        {
            return response.Content;
        }

        return new List<UserDto>();
    }
}