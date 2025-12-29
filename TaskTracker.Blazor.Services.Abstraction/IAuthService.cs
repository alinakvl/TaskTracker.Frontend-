using System.Security.Claims;
using TaskTracker.Blazor.Domain.DTOs.Auth;
using TaskTracker.Blazor.Domain.DTOs.Users;

namespace TaskTracker.Blazor.Services.Abstraction;

public interface IAuthService
{
    Task<bool> LoginAsync(LoginDto loginDto);
    Task<bool> RegisterAsync(RegisterDto registerDto);
    Task LogoutAsync();
    Task<string?> GetTokenAsync();
    Task<UserDto?> GetCurrentUserAsync();
    Task<bool> IsAuthenticatedAsync();

}


