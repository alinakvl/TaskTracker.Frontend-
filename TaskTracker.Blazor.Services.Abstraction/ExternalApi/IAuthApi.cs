using Refit;
using TaskTracker.Blazor.Domain.DTOs.Auth;

namespace TaskTracker.Blazor.Services.Abstraction.ExternalApi;

public interface IAuthApi
{
    [Post("/auth/login")]
    Task<AuthResponseDto> LoginAsync([Body] LoginDto loginDto);

    [Post("/auth/register")]
    Task<AuthResponseDto> RegisterAsync([Body] RegisterDto registerDto);
}