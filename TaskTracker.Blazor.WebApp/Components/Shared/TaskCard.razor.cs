using Microsoft.AspNetCore.Components;
using MudBlazor;
using TaskTracker.Blazor.Domain.DTOs.Tasks;

namespace TaskTracker.Blazor.WebApp.Components.Shared;

public partial class TaskCard
 {
    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    [Parameter]
    public TaskDto Task { get; set; } = null!;
    [Parameter]
    public EventCallback OnUpdated { get; set; }

private Color GetPriorityColor(int priority)
{
    return priority switch
    {
        4 => Color.Error,
        3 => Color.Warning,
        _ => Color.Default
    };
}

private Color GetDueDateColor(DateTime dueDate)
{
    var daysUntilDue = (dueDate - DateTime.UtcNow).Days;

    if (daysUntilDue < 0)
        return Color.Error;
    if (daysUntilDue <= 2)
        return Color.Warning;
    return Color.Default;
}
}
