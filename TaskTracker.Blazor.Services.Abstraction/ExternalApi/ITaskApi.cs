using Refit;
using TaskTracker.Blazor.Domain.DTOs.Tasks;

namespace TaskTracker.Blazor.Services.Abstraction.ExternalApi;

public interface ITaskApi
{
    [Get("/tasks/{id}")]
    Task<IApiResponse<TaskDetailDto>> GetTaskByIdAsync(Guid id);

    [Get("/tasks/list/{listId}")]
    Task<IApiResponse<List<TaskDto>>> GetTasksByListAsync(Guid listId);

    [Get("/tasks/my")]
    Task<IApiResponse<List<TaskDto>>> GetMyTasksAsync();

    [Post("/tasks")]
    Task<IApiResponse<TaskDto>> CreateTaskAsync([Body] CreateTaskDto createTaskDto);

    [Put("/tasks/{id}")]
    Task<IApiResponse<TaskDto>> UpdateTaskAsync(Guid id, [Body] UpdateTaskDto updateTaskDto);

    [Delete("/tasks/{id}")]
    Task<IApiResponse> DeleteTaskAsync(Guid id);
}
