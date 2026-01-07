using TaskTracker.Blazor.Domain.DTOs.Tasks;
namespace TaskTracker.Blazor.Domain.DTOs.TaskLists;

public class TaskListDto
{
    public Guid Id { get; set; }
    public Guid BoardId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Position { get; set; }
    public List<TaskDto> Tasks { get; set; } = new();
}
