namespace TaskTracker.Blazor.Domain.DTOs.TaskLists;

public class CreateTaskListDto
{
    public Guid BoardId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Position { get; set; }
}


