using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskTracker.Blazor.Domain.Constants;
using TaskTracker.Blazor.Domain.DTOs.Auth;
using TaskTracker.Blazor.Domain.DTOs.Users;
using TaskTracker.Blazor.Services.Abstraction;
using TaskTracker.Blazor.Services.Abstraction.ExternalApi;

namespace TaskTracker.Blazor.Services;

public class AuthService : IAuthService
{
    private readonly IAuthApi _authApi;
    private readonly IAppStorageService _localStorage;
    private UserDto? _currentUser;

    public AuthService(IAuthApi authApi, IAppStorageService localStorage)
    {
        _authApi = authApi;
        _localStorage = localStorage;
    }

    public async Task<bool> LoginAsync(LoginDto loginDto)
    {
        var response = await _authApi.LoginAsync(loginDto);

        if (response.IsSuccessStatusCode && response.Content != null)
        {
            return await HandleAuthResponse(response.Content);
        }

        return false;
    }

    public async Task<bool> RegisterAsync(RegisterDto registerDto)
    {
        var response = await _authApi.RegisterAsync(registerDto);

        if (response.IsSuccessStatusCode && response.Content != null)
        {
            return await HandleAuthResponse(response.Content);
        }

        return false;
    }

    private async Task<bool> HandleAuthResponse(AuthResponseDto response)
    {
        if (string.IsNullOrEmpty(response.Token))
            return false;

        await _localStorage.SetItemAsync(AppConstants.AuthTokenKey, response.Token);

        var user = ParseUserFromToken(response.Token);
        if (user != null)
        {
            await _localStorage.SetItemAsync(AppConstants.UserKey, user);
            _currentUser = user;
        }

        return true;
    }

    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync(AppConstants.AuthTokenKey);
        await _localStorage.RemoveItemAsync(AppConstants.UserKey);
        _currentUser = null;
    }

    public async Task<string?> GetTokenAsync()
    {
        return await _localStorage.GetItemAsync<string>(AppConstants.AuthTokenKey);
    }

    public async Task<UserDto?> GetCurrentUserAsync()
    {
        if (_currentUser != null)
            return _currentUser;

        _currentUser = await _localStorage.GetItemAsync<UserDto>(AppConstants.UserKey);
        return _currentUser;
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var token = await GetTokenAsync();
        return !string.IsNullOrEmpty(token);
    }

    private UserDto? ParseUserFromToken(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token)) return null;

            var jwtToken = handler.ReadJwtToken(token);
            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId" || c.Type == "sub" || c.Type == ClaimTypes.NameIdentifier)?.Value;
            var email = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email")?.Value;
            var role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role || c.Type == "role")?.Value;
            var firstName = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
            var lastName = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(email))
                return null;

            return new UserDto
            {
                Id = Guid.Parse(userId),
                Email = email,
                FirstName = firstName ?? "",
                LastName = lastName ?? "",
                Role = role ?? AppConstants.Roles.User
            };
        }
        catch
        {
            return null;
        }
    }
}
