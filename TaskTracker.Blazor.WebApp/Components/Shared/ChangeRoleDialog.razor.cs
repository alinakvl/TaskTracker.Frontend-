using Microsoft.AspNetCore.Components;
using MudBlazor;
using TaskTracker.Blazor.Domain.DTOs.Users;
using TaskTracker.Blazor.Services;
using TaskTracker.Blazor.Services.Abstraction;

namespace TaskTracker.Blazor.WebApp.Components.Shared;

public partial class ChangeRoleDialog
{
    [Inject]
    private IUserService UserService { get; set; } = default!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;

    [Parameter] public Guid UserId { get; set; }
    [Parameter] public string CurrentRole { get; set; } = string.Empty;
    [Parameter] public EventCallback<string?> OnClose { get; set; }

    private string selectedRole = string.Empty;
    private string errorMessage = string.Empty;
    private bool isSubmitting = false;

    protected override void OnInitialized()
    {
        selectedRole = CurrentRole;
    }

    private async Task Submit()
    {
        if (selectedRole == CurrentRole)
        {
            await OnClose.InvokeAsync(null);
            return;
        }

        isSubmitting = true;
        errorMessage = string.Empty;

        try
        {
            var roleDto = new ChangeRoleDto
            {
                Role = selectedRole
            };

            var success = await UserService.ChangeUserRoleAsync(UserId, roleDto);

            if (success)
            {
                await OnClose.InvokeAsync(selectedRole);
            }
            else
            {
                errorMessage = "Failed to update role on server.";
            }
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
        }
        finally
        {
            isSubmitting = false;
        }
    }

    private Task Cancel() => OnClose.InvokeAsync(null);
}