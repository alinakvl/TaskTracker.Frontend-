using Refit;
using TaskTracker.Blazor.Domain.DTOs.Users;

namespace TaskTracker.Blazor.Services.Abstraction.ExternalApi;

public interface IUserApi
{
    [Get("/users/{id}")]
    Task<UserDto> GetUserByIdAsync(Guid id);

    [Get("/users")]
    Task<List<UserDto>> GetAllUsersAsync();

    [Put("/users/{id}")]
    Task<UserDto> UpdateUserAsync(Guid id, [Body] UpdateUserDto updateUserDto);

    [Delete("/users/{id}")]
    Task DeleteUserAsync(Guid id);

    [Patch("/users/{userId}/role")]
    Task ChangeUserRoleAsync(Guid userId, [Body] ChangeRoleDto roleDto);
}
