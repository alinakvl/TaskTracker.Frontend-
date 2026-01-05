using Refit;
using TaskTracker.Blazor.Domain.DTOs.Auth;

namespace TaskTracker.Blazor.Services.Abstraction.ExternalApi;

public interface IAuthApi
{
    [Post("/auth/login")]
    Task<IApiResponse<AuthResponseDto>> LoginAsync([Body] LoginDto loginDto);

    [Post("/auth/register")]
    Task<IApiResponse<AuthResponseDto>> RegisterAsync([Body] RegisterDto registerDto);
}