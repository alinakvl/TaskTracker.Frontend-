using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using TaskTracker.Blazor.Domain.DTOs.Auth;
using TaskTracker.Blazor.Services;
using TaskTracker.Blazor.Services.Abstraction;
using TaskTracker.Blazor.WebApp.Authentication;

namespace TaskTracker.Blazor.WebApp.Pages;

public partial class Register

{
    [Inject]
    private IAuthService AuthService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;

    [Inject]
    private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;
    private RegisterDto registerModel = new();
private bool isLoading = false;

private async Task HandleRegister()
{
    isLoading = true;

    try
    {

        var success = await AuthService.RegisterAsync(registerModel);

        if (success)
        {

            var loginModel = new LoginDto
            {
                Email = registerModel.Email,
                Password = registerModel.Password
            };

            var loginSuccess = await AuthService.LoginAsync(loginModel);

            if (loginSuccess)
            {

                if (AuthStateProvider is CustomAuthStateProvider customAuthProvider)
                {
                    customAuthProvider.NotifyAuthenticationStateChanged();
                }

                Snackbar.Add("Registration successful! Welcome!", Severity.Success);
                Navigation.NavigateTo("/");
            }
            else
            {

                Snackbar.Add("Account created. Please sign in.", Severity.Success);
                Navigation.NavigateTo("/login");
            }
        }
        else
        {
            Snackbar.Add("Registration failed. Email might be taken.", Severity.Error);
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
