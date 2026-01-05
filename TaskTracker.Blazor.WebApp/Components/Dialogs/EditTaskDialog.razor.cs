using Microsoft.AspNetCore.Components;
using TaskTracker.Blazor.Domain.DTOs.Tasks;

namespace TaskTracker.Blazor.WebApp.Components.Dialogs;

public partial class EditTaskDialog
 {
   [Parameter] public TaskDto Task { get; set; } = new();
   [Parameter] public EventCallback<UpdateTaskDto?> OnClose { get; set; }

private UpdateTaskDto model = new();
private DateTime? dueDate;
private string errorMessage = string.Empty;

protected override void OnInitialized()
{
    model = new UpdateTaskDto
    {
        Id = Task.Id,
        Title = Task.Title,
        Description = Task.Description,
        Priority = Task.Priority
    };
    dueDate = Task.DueDate;
}

private async Task Submit()
{
    if (string.IsNullOrWhiteSpace(model.Title))
    {
        errorMessage = "Title is required";
        return;
    }

    model.DueDate = dueDate;
    await OnClose.InvokeAsync(model);
}

private Task Cancel() => OnClose.InvokeAsync(null);
}
