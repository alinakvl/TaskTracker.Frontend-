namespace TaskTracker.Blazor.Domain.DTOs.Tasks;

public class MoveTaskDto
{
    public Guid TargetListId { get; set; }
    public int Position { get; set; }
}

