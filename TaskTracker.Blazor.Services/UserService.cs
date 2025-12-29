using TaskTracker.Blazor.Domain.DTOs.Users;
using TaskTracker.Blazor.Services.Abstraction;

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
        try
        {
            return await _userApi.GetAllUsersAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting users: {ex.Message}");
            return new List<UserDto>();
        }
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        try
        {
            return await _userApi.GetUserByIdAsync(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting user: {ex.Message}");
            return null;
        }
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        try
        {
            var users = await GetAllUsersAsync();
            return users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting user by email: {ex.Message}");
            return null;
        }
    }

    public async Task<UserDto?> UpdateUserAsync(Guid id, UpdateUserDto updateUserDto)
    {
        try
        {
            return await _userApi.UpdateUserAsync(id, updateUserDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating user: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        try
        {
            await _userApi.DeleteUserAsync(id);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting user: {ex.Message}");
            return false;
        }
    }

    //public async Task<bool> ChangeUserRoleAsync(Guid userId, string role)
    //{
    //    try
    //    {
    //        await _userApi.ChangeUserRoleAsync(userId, role);
    //        return true;
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine($"Error changing user role: {ex.Message}");
    //        return false;
    //    }
    //}
    public async Task<bool> ChangeUserRoleAsync(Guid userId, ChangeRoleDto roleDto)
    {
        try
        {
            await _userApi.ChangeUserRoleAsync(userId, roleDto);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error changing user role: {ex.Message}");
            return false;
        }
    }
}