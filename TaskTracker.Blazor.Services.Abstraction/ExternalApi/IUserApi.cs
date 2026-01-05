using Refit;
using TaskTracker.Blazor.Domain.DTOs.Users;

namespace TaskTracker.Blazor.Services.Abstraction.ExternalApi;

public interface IUserApi
{
    [Get("/users/{id}")]
    [Headers("Authorization: Bearer")]
    Task<UserDto> GetUserByIdAsync(Guid id);

    [Get("/users")]
    [Headers("Authorization: Bearer")]
    Task<List<UserDto>> GetAllUsersAsync();

    [Put("/users/{id}")]
    [Headers("Authorization: Bearer")]
    Task<UserDto> UpdateUserAsync(Guid id, [Body] UpdateUserDto updateUserDto);

    [Delete("/users/{id}")]
    [Headers("Authorization: Bearer")]
    Task DeleteUserAsync(Guid id);

    [Patch("/users/{userId}/role")]
    [Headers("Authorization: Bearer")]
    Task ChangeUserRoleAsync(Guid userId, [Body] ChangeRoleDto roleDto);
}
