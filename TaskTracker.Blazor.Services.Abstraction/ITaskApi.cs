using Refit;
using TaskTracker.Blazor.Domain.DTOs.Tasks;

namespace TaskTracker.Blazor.Services.Abstraction;

public interface ITaskApi
{
    [Get("/tasks/{id}")]
    [Headers("Authorization: Bearer")]
    Task<TaskDetailDto> GetTaskByIdAsync(Guid id);

    [Get("/tasks/list/{listId}")]
    [Headers("Authorization: Bearer")]
    Task<List<TaskDto>> GetTasksByListAsync(Guid listId);

    [Get("/tasks/my")]
    [Headers("Authorization: Bearer")]
    Task<List<TaskDto>> GetMyTasksAsync();

    [Post("/tasks")]
    [Headers("Authorization: Bearer")]
    Task<TaskDto> CreateTaskAsync([Body] CreateTaskDto createTaskDto);

    [Put("/tasks/{id}")]
    [Headers("Authorization: Bearer")]
    Task<TaskDto> UpdateTaskAsync(Guid id, [Body] UpdateTaskDto updateTaskDto);

    [Delete("/tasks/{id}")]
    [Headers("Authorization: Bearer")]
    Task DeleteTaskAsync(Guid id);
}
