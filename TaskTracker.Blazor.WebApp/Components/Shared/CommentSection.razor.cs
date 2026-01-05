using Microsoft.AspNetCore.Components;
using MudBlazor;
using TaskTracker.Blazor.Domain.DTOs.Comments;
using TaskTracker.Blazor.Services.Abstraction;

namespace TaskTracker.Blazor.WebApp.Components.Shared;

public partial class CommentSection
 {
    [Inject]
    private ICommentService CommentService { get; set; } = default!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;

    [Inject]
    private IAuthService AuthService { get; set; } = default!;
    [Parameter]
    public Guid TaskId { get; set; }

    [Parameter]
    public List<CommentDto> Comments { get; set; } = new();

private string newComment = string.Empty;

private async Task AddComment()
{
    if (string.IsNullOrWhiteSpace(newComment))
        return;

    var createDto = new CreateCommentDto
    {
        TaskId = TaskId,
        Content = newComment
    };

    var created = await CommentService.CreateCommentAsync(createDto);

    if (created != null)
    {

        if (string.IsNullOrEmpty(created.UserName))
        {

            var currentUser = await AuthService.GetCurrentUserAsync();

            if (currentUser != null)
            {

                created.UserName = $"{currentUser.FirstName} {currentUser.LastName}";
            }
            else
            {

                created.UserName = "Me";
            }
        }

        Comments.Add(created);
        newComment = string.Empty;
        Snackbar.Add("Comment added", Severity.Success);
    }
    else
    {
        Snackbar.Add("Failed to add comment", Severity.Error);
    }
}

private string GetTimeAgo(DateTime dateTime)
{
    var timeSpan = DateTime.UtcNow - dateTime;

    if (timeSpan.TotalMinutes < 1)
        return "just now";
    if (timeSpan.TotalMinutes < 60)
        return $"{(int)timeSpan.TotalMinutes}m ago";
    if (timeSpan.TotalHours < 24)
        return $"{(int)timeSpan.TotalHours}h ago";
    if (timeSpan.TotalDays < 7)
        return $"{(int)timeSpan.TotalDays}d ago";

    return dateTime.ToString("MMM dd, yyyy");
}
}

