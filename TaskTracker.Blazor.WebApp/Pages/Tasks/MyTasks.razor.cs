using Microsoft.AspNetCore.Components;
using MudBlazor;
using TaskTracker.Blazor.Domain.DTOs.Tasks;
using TaskTracker.Blazor.Services.Abstraction;

namespace TaskTracker.Blazor.WebApp.Pages.Tasks;

public partial class MyTasks
{
    [Inject]
    private ITaskService TaskService { get; set; } = default!;

    private List<TaskDto> tasks = new();
    private bool isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        isLoading = true;
        tasks = await TaskService.GetMyTasksAsync();
        isLoading = false;
    }

    private Color GetPriorityColor(int priority)
    {
        return priority switch
        {
            4 => Color.Error,
            3 => Color.Warning,
            2 => Color.Info,
            _ => Color.Default
        };
    }
}
