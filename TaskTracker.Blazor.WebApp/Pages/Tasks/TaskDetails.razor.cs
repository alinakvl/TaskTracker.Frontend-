using Microsoft.AspNetCore.Components;
using MudBlazor;
using TaskTracker.Blazor.Domain.DTOs.Tasks;
using TaskTracker.Blazor.Services.Abstraction;

namespace TaskTracker.Blazor.WebApp.Pages.Tasks;

public partial class TaskDetails
{
    [Inject]
    private ITaskService TaskService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;

    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    [Parameter]
    public Guid TaskId { get; set; }

    private TaskDetailDto? task;
    private bool isLoading = true;
    private bool showEditDialog = false;

    protected override async Task OnInitializedAsync() => await LoadTask();

    private async Task LoadTask()
    {
        try
        {
            isLoading = true;
            task = await TaskService.GetTaskByIdAsync(TaskId);
        }
        catch (Exception ex)
        {
            Snackbar.Add($"Error loading task: {ex.Message}", Severity.Error);
        }
        finally
        {
            isLoading = false;
        }
    }

    private void OpenEditDialog() => showEditDialog = true;

    private async Task HandleEditTaskClose(UpdateTaskDto? updateDto)
    {
        showEditDialog = false;

        if (updateDto != null)
        {
            try
            {
                var result = await TaskService.UpdateTaskAsync(updateDto.Id, updateDto);

                if (result != null)
                {
                    if (task != null)
                    {
                        task.Title = updateDto.Title;
                        task.Description = updateDto.Description;
                        task.Priority = updateDto.Priority;
                        task.DueDate = updateDto.DueDate;
                        task.PriorityName = GetPriorityName(updateDto.Priority);
                    }

                    await LoadTask();
                    StateHasChanged();
                    Snackbar.Add("Task updated successfully!", Severity.Success);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Error: {ex.Message}", Severity.Error);
            }
        }
    }

    private string GetPriorityName(int p) => p switch
    {
        1 => "Low",
        2 => "Medium",
        3 => "High",
        4 => "Critical",
        _ => "Unknown"
    };

    private async Task DeleteTask()
    {
        bool? result = await DialogService.ShowMessageBox(
            "Confirm Delete",
            $"Are you sure you want to delete '{task?.Title}'?",
            yesText: "Delete", cancelText: "Cancel");

        if (result == true)
        {
            var success = await TaskService.DeleteTaskAsync(TaskId);

            if (success)
            {
                Snackbar.Add("Task deleted successfully", Severity.Success);
                Navigation.NavigateTo("/boards");
            }
            else
            {
                Snackbar.Add("Failed to delete task. Check your permissions.", Severity.Error);
            }
        }
    }

    private TaskDto MapToTaskDto(TaskDetailDto detail) => new TaskDto
    {
        Id = detail.Id,
        Title = detail.Title,
        Description = detail.Description,
        Priority = detail.Priority,
        DueDate = detail.DueDate
    };

    private Color GetPriorityColor(int priority) => priority switch
    {
        4 => Color.Error,
        3 => Color.Warning,
        2 => Color.Info,
        _ => Color.Success
    };
}