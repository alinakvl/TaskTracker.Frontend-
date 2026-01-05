using Refit;
using TaskTracker.Blazor.Domain.DTOs.Tasks;

namespace TaskTracker.Blazor.Services.Abstraction.ExternalApi;

public interface ITaskApi
{
    [Get("/tasks/{id}")]
    Task<TaskDetailDto> GetTaskByIdAsync(Guid id);

    [Get("/tasks/list/{listId}")]
    Task<List<TaskDto>> GetTasksByListAsync(Guid listId);

    [Get("/tasks/my")]
    Task<List<TaskDto>> GetMyTasksAsync();

    [Post("/tasks")]
    Task<TaskDto> CreateTaskAsync([Body] CreateTaskDto createTaskDto);

    [Put("/tasks/{id}")]
    Task<TaskDto> UpdateTaskAsync(Guid id, [Body] UpdateTaskDto updateTaskDto);

    [Delete("/tasks/{id}")]
    Task DeleteTaskAsync(Guid id);
}
