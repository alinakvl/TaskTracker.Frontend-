using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using TaskTracker.Blazor.Services.Abstraction;

namespace TaskTracker.Blazor.WebApp.Layout;

public partial class LoginDisplay
{
    [Inject]
    private IAuthService AuthService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    [Inject]
    private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;

    private async Task Logout()
    {
        await AuthService.LogoutAsync();

        if (AuthStateProvider is TaskTracker.Blazor.WebApp.Authentication.CustomAuthStateProvider customProvider)
        {
            customProvider.NotifyAuthenticationStateChanged();
        }

        Navigation.NavigateTo("/", forceLoad: true);
    }
}
