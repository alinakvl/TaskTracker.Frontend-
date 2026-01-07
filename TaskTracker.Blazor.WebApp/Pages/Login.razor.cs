using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using TaskTracker.Blazor.Domain.DTOs.Auth;
using TaskTracker.Blazor.Services.Abstraction;
using TaskTracker.Blazor.WebApp.Authentication;

namespace TaskTracker.Blazor.WebApp.Pages;

public partial class Login
{
    [Inject]
    private IAuthService AuthService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;

    [Inject]
    private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;

    private LoginDto loginModel = new();
    private bool isLoading = false;

    private async Task HandleLogin()
    {
        isLoading = true;
        try
        {
            var success = await AuthService.LoginAsync(loginModel);

            if (success)
            {
                if (AuthStateProvider is CustomAuthStateProvider customAuthProvider)
                {
                    customAuthProvider.NotifyAuthenticationStateChanged();
                }

                Snackbar.Add("Login successful!", Severity.Success);
                Navigation.NavigateTo("/");
            }
            else
            {
                Snackbar.Add("Invalid email or password", Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add($"An error occurred: {ex.Message}", Severity.Error);
        }
        finally
        {
            isLoading = false;
        }
    }
}
