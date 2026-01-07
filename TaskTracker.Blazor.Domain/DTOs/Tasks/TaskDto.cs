using TaskTracker.Blazor.Domain.DTOs.Labels;

namespace TaskTracker.Blazor.Domain.DTOs.Tasks;

public class TaskDto
{
    public Guid Id { get; set; }
    public Guid ListId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid? AssignedUserId { get; set; }
    public string? AssignedUserName { get; set; }
    public string? AssignedUserAvatar { get; set; }
    public int Position { get; set; }
    public DateTime? DueDate { get; set; }
    public int Priority { get; set; }
    public string PriorityName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int CommentsCount { get; set; }
    public int AttachmentsCount { get; set; }
    public List<LabelDto> Labels { get; set; } = new();
}

