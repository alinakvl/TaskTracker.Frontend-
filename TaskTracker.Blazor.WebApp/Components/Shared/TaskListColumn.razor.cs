using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using TaskTracker.Blazor.Domain.DTOs.TaskLists;
using TaskTracker.Blazor.Domain.DTOs.Tasks;
using TaskTracker.Blazor.Services.Abstraction;

namespace TaskTracker.Blazor.WebApp.Components.Shared;

public partial class TaskListColumn
 {
    [Inject]
    private ITaskListService TaskListService { get; set; } = default!;

    [Inject]
    private ITaskService TaskService { get; set; } = default!;

    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;
    [Parameter] public TaskListDto TaskList { get; set; } = null!;
    [Parameter] public Guid BoardId { get; set; }
    [Parameter] public EventCallback OnTaskUpdated { get; set; }
    [Parameter] public EventCallback OnListDeleted { get; set; }

private bool isEditingTitle = false;
private bool showAddTask = false;
private string editedTitle = string.Empty;
private string newTaskTitle = string.Empty;
private MudTextField<string>? titleField;

private async Task StartEditTitle()
{
    editedTitle = TaskList.Title;
    isEditingTitle = true;
    await Task.Delay(100);
    if (titleField != null)
    {
        await titleField.FocusAsync();
    }
}

private async Task SaveTitle()
{
    if (string.IsNullOrWhiteSpace(editedTitle))
    {
        editedTitle = TaskList.Title;
        isEditingTitle = false;
        return;
    }

    if (editedTitle.Trim() != TaskList.Title)
    {
        var updateDto = new UpdateTaskListDto
        {
            Title = editedTitle.Trim(),
            Position = TaskList.Position
        };

        var updated = await TaskListService.UpdateTaskListAsync(TaskList.Id, updateDto);
        if (updated != null)
        {
            Snackbar.Add("List renamed", Severity.Success);
            await OnTaskUpdated.InvokeAsync();
        }
        else
        {
            Snackbar.Add("Failed to rename list", Severity.Error);
        }
    }

    isEditingTitle = false;
}

private async Task HandleTitleKeyDown(KeyboardEventArgs e)
{
    if (e.Key == "Enter")
    {
        await SaveTitle();
    }
    else if (e.Key == "Escape")
    {
        isEditingTitle = false;
    }
}
private async Task DeleteList()
{
    var result = await DialogService.ShowMessageBox(
        "Delete List",
        $"Are you sure you want to delete '{TaskList.Title}'? All tasks in this list will be deleted.",
        yesText: "Delete",
        cancelText: "Cancel");

    if (result == true)
    {
        try
        {

            var success = await TaskListService.DeleteTaskListAsync(TaskList.Id);

            if (success)
            {
                await Task.Delay(100);

                Snackbar.Add("List deleted", Severity.Success);

                await OnListDeleted.InvokeAsync();
            }
            else
            {
                Snackbar.Add("Failed to delete list", Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add($"Error: {ex.Message}", Severity.Error);
            Console.WriteLine($"Delete error: {ex}");
        }
    }
}
private async Task AddTask()
{
    if (string.IsNullOrWhiteSpace(newTaskTitle))
    {
        Snackbar.Add("Task title is required", Severity.Warning);
        return;
    }

    try
    {
        var createTaskDto = new CreateTaskDto
        {
            ListId = TaskList.Id,
            Title = newTaskTitle.Trim(),
            Description = "",
            Priority = 2,
            AssignedUserId = null,
            DueDate = null
        };


        var createdTask = await TaskService.CreateTaskAsync(createTaskDto);

        if (createdTask != null)
        {
            Snackbar.Add("Task added successfully", Severity.Success);
            newTaskTitle = string.Empty;
            showAddTask = false;
            await OnTaskUpdated.InvokeAsync();
        }
    }
    catch (Exception ex)
    {
        Snackbar.Add($"Error: {ex.Message}", Severity.Error);
    }
}
}
