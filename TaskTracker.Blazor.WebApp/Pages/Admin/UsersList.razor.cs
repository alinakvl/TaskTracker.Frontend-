using Microsoft.AspNetCore.Components;
using MudBlazor;
using TaskTracker.Blazor.Domain.DTOs.Users;
using TaskTracker.Blazor.Services.Abstraction;

namespace TaskTracker.Blazor.WebApp.Pages.Admin;

public partial class UsersList
 {
    [Inject]
    private IUserService UserService { get; set; } = default!;

    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    private List<UserDto> users = new();
private string searchString = string.Empty;
private bool isLoading = true;
private bool showEditDialog = false;
private bool showRoleDialog = false;
private UserDto? selectedUser;

private IEnumerable<UserDto> FilteredUsers => users.Where(u =>
    string.IsNullOrWhiteSpace(searchString) ||
    u.FullName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
    u.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase));

protected override async Task OnInitializedAsync() => await LoadUsers();

private async Task LoadUsers()
{
    isLoading = true;
    users = await UserService.GetAllUsersAsync();
    isLoading = false;
}

private void OpenEditUser(UserDto user)
{
    selectedUser = user;
    showEditDialog = true;
}

private void OpenChangeRole(UserDto user)
{
    selectedUser = user;
    showRoleDialog = true;
}

private async Task HandleEditDialogClose(UserDto? updated)
{
    showEditDialog = false;
    if (updated != null)
    {
        await LoadUsers();
        Snackbar.Add("User updated successfully", Severity.Success);
    }
}

private async Task HandleRoleDialogClose(string? newRole)
{
    showRoleDialog = false;
    if (newRole != null)
    {
        await LoadUsers();
        Snackbar.Add($"Role changed to {newRole}", Severity.Success);
    }
}

private async Task DeleteUser(UserDto user)
{
    bool? result = await DialogService.ShowMessageBox(
        "Confirm Delete",
        $"Are you sure you want to delete {user.FullName}?",
        yesText: "Delete",
        cancelText: "Cancel");

    if (result == true)
    {
        try
        {
            var userToRemove = users.FirstOrDefault(u => u.Id == user.Id);
            if (userToRemove != null)
            {
                users.Remove(userToRemove);
                StateHasChanged();
            }


            var success = await UserService.DeleteUserAsync(user.Id);

            if (success)
            {
                Snackbar.Add("User deleted successfully", Severity.Success);
            }
            else
            {
                Snackbar.Add("Failed to delete user on server", Severity.Error);
                await LoadUsers();
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add($"Error: {ex.Message}", Severity.Error);
            await LoadUsers();
        }
    }
}

private Color GetRoleColor(string role) => role switch
{
    "Admin" => Color.Error,
    "User" => Color.Success,
    "Guest" => Color.Info,
    _ => Color.Default
};

private int GetUserCountByRole(string role) => users.Count(u => u.Role == role);
} 
