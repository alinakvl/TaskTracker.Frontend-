using Refit;
using TaskTracker.Blazor.Domain.DTOs.Users;

namespace TaskTracker.Blazor.Services.Abstraction.ExternalApi;

public interface IUserApi
{
    [Get("/users/{id}")]
    Task<IApiResponse<UserDto>> GetUserByIdAsync(Guid id);

    [Get("/users")]
    Task<IApiResponse<List<UserDto>>> GetAllUsersAsync();

    [Put("/users/{id}")]
    Task<IApiResponse<UserDto>> UpdateUserAsync(Guid id, [Body] UpdateUserDto updateUserDto);

    [Delete("/users/{id}")]
    Task<IApiResponse> DeleteUserAsync(Guid id);

    [Patch("/users/{userId}/role")]
    Task<IApiResponse> ChangeUserRoleAsync(Guid userId, [Body] ChangeRoleDto roleDto);
}
