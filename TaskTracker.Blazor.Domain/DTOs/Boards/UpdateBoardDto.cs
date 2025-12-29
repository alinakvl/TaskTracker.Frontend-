namespace TaskTracker.Blazor.Domain.DTOs.Boards;

public class UpdateBoardDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? BackgroundColor { get; set; }
}
