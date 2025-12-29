using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskTracker.Blazor.Domain.Constants;
using TaskTracker.Blazor.Domain.DTOs.Auth;
using TaskTracker.Blazor.Domain.DTOs.Users;
using TaskTracker.Blazor.Services.Abstraction;

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
        try
        {
            var response = await _authApi.LoginAsync(loginDto);

            if (!string.IsNullOrEmpty(response.Token))
            {
                await _localStorage.SetItemAsync(AppConstants.AuthTokenKey, response.Token);

                var user = ParseUserFromToken(response.Token);
                if (user != null)
                {
                    await _localStorage.SetItemAsync(AppConstants.UserKey, user);
                    _currentUser = user;
                }

                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Login error: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> RegisterAsync(RegisterDto registerDto)
    {
        try
        {
            var response = await _authApi.RegisterAsync(registerDto);

            if (!string.IsNullOrEmpty(response.Token))
            {
                await _localStorage.SetItemAsync(AppConstants.AuthTokenKey, response.Token);

                var user = ParseUserFromToken(response.Token);
                if (user != null)
                {
                    await _localStorage.SetItemAsync(AppConstants.UserKey, user);
                    _currentUser = user;
                }

                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Registration error: {ex.Message}");
            return false;
        }
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

        try
        {
            _currentUser = await _localStorage.GetItemAsync<UserDto>(AppConstants.UserKey);
            return _currentUser;
        }
        catch
        {
            return null;
        }
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
            var jwtToken = handler.ReadJwtToken(token);

            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId" || c.Type == "sub")?.Value;
            var email = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
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
        catch (Exception ex)
        {
            Console.WriteLine($"Token parsing error: {ex.Message}");
            return null;
        }
    }
}
