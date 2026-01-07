using TaskTracker.Blazor.Domain.DTOs.Tasks;

namespace TaskTracker.Blazor.Services.Abstraction;

public interface ITaskService
{
    Task<TaskDetailDto?> GetTaskByIdAsync(Guid id);
    Task<List<TaskDto>> GetTasksByListAsync(Guid listId);
    Task<List<TaskDto>> GetMyTasksAsync();
    Task<TaskDto?> CreateTaskAsync(CreateTaskDto createTaskDto);
    Task<TaskDto?> UpdateTaskAsync(Guid id, UpdateTaskDto updateTaskDto);
    Task<bool> DeleteTaskAsync(Guid id);
}

