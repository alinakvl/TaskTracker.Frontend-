namespace TaskTracker.Blazor.Domain.DTOs.Labels;

public class CreateLabelDto
{
    public Guid BoardId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = "#61BD4F";
}

