//using System.Security.Claims;
//using Microsoft.AspNetCore.Components.Authorization;
//using TaskTracker.Blazor.Services.Abstraction;
//using TaskTracker.Blazor.Domain.Constants;

//namespace TaskTracker.Blazor.WebApp.Authentication;

//public class CustomAuthStateProvider : AuthenticationStateProvider
//{
//    private readonly IAuthService _authService;

//    public CustomAuthStateProvider(IAuthService authService)
//    {
//        _authService = authService;
//    }

//    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
//    {
//        var user = await _authService.GetCurrentUserAsync();

//        if (user == null)
//        {
//            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
//        }

//        var claims = new List<Claim>
//        {
//            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//            new Claim(ClaimTypes.Email, user.Email),
//            new Claim(ClaimTypes.Name, user.FullName),
//            new Claim(ClaimTypes.Role, user.Role)
//        };

//        var identity = new ClaimsIdentity(claims, "jwt");
//        var claimsPrincipal = new ClaimsPrincipal(identity);

//        return new AuthenticationState(claimsPrincipal);
//    }

//    public void NotifyAuthenticationStateChanged()
//    {
//        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
//    }
//}


using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace TaskTracker.Blazor.WebApp.Authentication;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _httpClient;

    public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient httpClient)
    {
        _localStorage = localStorage;
        _httpClient = httpClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");

        if (string.IsNullOrWhiteSpace(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


        var claims = ParseClaimsFromJwt(token);

        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }


    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        var claims = new List<Claim>();

        if (keyValuePairs != null)
        {
            foreach (var kvp in keyValuePairs)
            {
                if (kvp.Value is JsonElement element && element.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in element.EnumerateArray())
                    {
                        claims.Add(new Claim(kvp.Key, item.ToString()));
                    }
                }
                else
                {
                    claims.Add(new Claim(kvp.Key, kvp.Value.ToString()!));
                }
            }
        }

        return claims;
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}




