using TaskTracker.Blazor.Domain.DTOs.TaskLists;
using TaskTracker.Blazor.Domain.DTOs.Labels;

namespace TaskTracker.Blazor.Domain.DTOs.Boards;

public class BoardDetailDto : BoardDto
{
    public List<TaskListDto> TaskLists { get; set; } = new();
    public List<LabelDto> Labels { get; set; } = new();
    public List<BoardMemberDto> Members { get; set; } = new();
}
