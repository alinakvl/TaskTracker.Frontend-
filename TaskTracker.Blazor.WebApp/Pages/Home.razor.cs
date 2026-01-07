using Microsoft.AspNetCore.Components;
using MudBlazor;
using TaskTracker.Blazor.Domain.DTOs.Boards;
using TaskTracker.Blazor.Domain.DTOs.Tasks;
using TaskTracker.Blazor.Domain.DTOs.Users;
using TaskTracker.Blazor.Services.Abstraction;

namespace TaskTracker.Blazor.WebApp.Pages;

public partial class Home
{
    [Inject]
    private IBoardService BoardService { get; set; } = default!;

    [Inject]
    private ITaskService TaskService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    [Inject]
    private IAuthService AuthService { get; set; } = default!;

    private List<BoardDto> boards = new();
    private List<TaskDto> myTasks = new();
    private List<TaskDto> upcomingTasks = new();
    private UserDto? currentUser;
    private bool isLoading = true;

    private int completedTasksCount = 0;
    private int overdueTasksCount = 0;

    protected override async Task OnInitializedAsync()
    {
        await LoadDashboardData();
    }

    private async Task LoadDashboardData()
    {
        isLoading = true;

        try
        {
            currentUser = await AuthService.GetCurrentUserAsync();
            boards = await BoardService.GetMyBoardsAsync();
            myTasks = await TaskService.GetMyTasksAsync();

            if (myTasks != null)
            {
                completedTasksCount = myTasks.Count(t => t.PriorityName == "Done");
                overdueTasksCount = myTasks.Count(t => t.DueDate.HasValue && t.DueDate.Value < DateTime.UtcNow);

                upcomingTasks = myTasks
                    .Where(t => t.DueDate.HasValue && t.DueDate.Value > DateTime.UtcNow)
                    .OrderBy(t => t.DueDate)
                    .ToList();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading dashboard: {ex.Message}");
        }
        finally
        {
            isLoading = false;
        }
    }

    private string GetGreetingMessage()
    {
        var hour = DateTime.Now.Hour;
        if (hour < 12) return "Good morning! Here's what's happening with your projects.";
        if (hour < 18) return "Good afternoon! Let's stay productive.";
        return "Good evening! Time to wrap up today's tasks.";
    }

    private Color GetPriorityColor(int priority)
    {
        return priority switch
        {
            4 => Color.Error,
            3 => Color.Warning,
            2 => Color.Info,
            _ => Color.Success
        };
    }

    private Color GetDueDateColor(DateTime dueDate)
    {
        var daysUntilDue = (dueDate - DateTime.UtcNow).Days;
        if (daysUntilDue < 0) return Color.Error;
        if (daysUntilDue <= 2) return Color.Warning;
        return Color.Default;
    }

    private Color GetDueDateTextColor(DateTime dueDate)
    {
        var daysUntilDue = (dueDate - DateTime.UtcNow).Days;
        if (daysUntilDue < 0) return Color.Error;
        if (daysUntilDue <= 2) return Color.Warning;
        return Color.Default;
    }

    private string GetDueDateText(DateTime dueDate)
    {
        var daysUntilDue = (dueDate - DateTime.UtcNow).Days;
        if (daysUntilDue < 0) return $"Overdue by {Math.Abs(daysUntilDue)} days";
        if (daysUntilDue == 0) return "Due today!";
        if (daysUntilDue == 1) return "Due tomorrow";
        if (daysUntilDue <= 7) return $"Due in {daysUntilDue} days";
        return dueDate.ToString("MMM dd, yyyy");
    }

    private int GetTaskCountByPriority(int priority)
    {
        if (myTasks == null) return 0;
        return myTasks.Count(t => t.Priority == priority);
    }

    private double GetTaskPercentageByPriority(int priority)
    {
        if (myTasks == null || myTasks.Count == 0) return 0;
        return (double)GetTaskCountByPriority(priority) / myTasks.Count * 100;
    }
}
