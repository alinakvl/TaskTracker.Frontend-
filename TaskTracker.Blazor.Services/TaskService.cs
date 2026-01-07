using TaskTracker.Blazor.Domain.DTOs.Tasks;
using TaskTracker.Blazor.Services.Abstraction;
using TaskTracker.Blazor.Services.Abstraction.ExternalApi;

namespace TaskTracker.Blazor.Services;

public class TaskService : ITaskService
{
    private readonly ITaskApi _taskApi;

    public TaskService(ITaskApi taskApi)
    {
        _taskApi = taskApi;
    }

    public async Task<TaskDetailDto?> GetTaskByIdAsync(Guid id)
    {
        var response = await _taskApi.GetTaskByIdAsync(id);

        if (response.IsSuccessStatusCode)
        {
            return response.Content;
        }

        return null;
    }

    public async Task<List<TaskDto>> GetTasksByListAsync(Guid listId)
    {
        var response = await _taskApi.GetTasksByListAsync(listId);

        if (response.IsSuccessStatusCode && response.Content != null)
        {
            return response.Content;
        }

        return new List<TaskDto>();
    }

    public async Task<List<TaskDto>> GetMyTasksAsync()
    {
        var response = await _taskApi.GetMyTasksAsync();

        if (response.IsSuccessStatusCode && response.Content != null)
        {
            return response.Content;
        }

        return new List<TaskDto>();
    }

    public async Task<TaskDto?> CreateTaskAsync(CreateTaskDto createTaskDto)
    {
        var response = await _taskApi.CreateTaskAsync(createTaskDto);

        if (response.IsSuccessStatusCode)
        {
            return response.Content;
        }

        return null;
    }

    public async Task<TaskDto?> UpdateTaskAsync(Guid id, UpdateTaskDto updateTaskDto)
    {
        var response = await _taskApi.UpdateTaskAsync(id, updateTaskDto);

        if (response.IsSuccessStatusCode)
        {
            return response.Content;
        }

        return null;
    }

    public async Task<bool> DeleteTaskAsync(Guid id)
    {
        var response = await _taskApi.DeleteTaskAsync(id);
        return response.IsSuccessStatusCode;
    }
}