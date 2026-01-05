using Microsoft.AspNetCore.Components;
using TaskTracker.Blazor.Domain.DTOs.Users;
using TaskTracker.Blazor.Services.Abstraction;

namespace TaskTracker.Blazor.WebApp.Components.Shared;

public partial class EditUserDialog
 {
    [Inject]
    private IUserService UserService { get; set; } = default!;
    [Parameter] public Guid UserId { get; set; }
    [Parameter] public UserDto? ExistingUser { get; set; }
    [Parameter] public EventCallback<UserDto?> OnClose { get; set; }

private UpdateUserDto model = new();
private string errorMessage = string.Empty;
private bool isSubmitting = false;

protected override void OnInitialized()
{
    if (ExistingUser != null)
    {
        model.FirstName = ExistingUser.FirstName;
        model.LastName = ExistingUser.LastName;
        model.AvatarUrl = ExistingUser.AvatarUrl;
    }
}

private async Task Submit()
{
    errorMessage = string.Empty;
    if (string.IsNullOrWhiteSpace(model.FirstName) || string.IsNullOrWhiteSpace(model.LastName))
    {
        errorMessage = "First and Last names are required";
        return;
    }

    isSubmitting = true;
    try
    {
        var updated = await UserService.UpdateUserAsync(UserId, model);
        if (updated != null)
        {
            await OnClose.InvokeAsync(updated);
        }
        else
        {
            errorMessage = "Failed to update user";
        }
    }
    catch (Exception ex)
    {
        errorMessage = $"Error: {ex.Message}";
    }
    finally
    {
        isSubmitting = false;
    }
}

private Task Cancel() => OnClose.InvokeAsync(null);
}
