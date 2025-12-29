using TaskTracker.Blazor.Domain.DTOs.Comments;
using TaskTracker.Blazor.Domain.DTOs.Attachments;

namespace TaskTracker.Blazor.Domain.DTOs.Tasks;

public class TaskDetailDto : TaskDto
{
    public List<CommentDto> Comments { get; set; } = new();
    public List<AttachmentDto> Attachments { get; set; } = new();
}
