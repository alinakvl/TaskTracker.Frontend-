using TaskTracker.Blazor.Domain.DTOs.Tasks;
using TaskTracker.Blazor.Services.Abstraction;

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
        try
        {
            return await _taskApi.GetTaskByIdAsync(id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<TaskDto>> GetTasksByListAsync(Guid listId)
    {
        try
        {
            return await _taskApi.GetTasksByListAsync(listId);
        }
        catch
        {
            return new List<TaskDto>();
        }
    }

    public async Task<List<TaskDto>> GetMyTasksAsync()
    {
        try
        {
            return await _taskApi.GetMyTasksAsync();
        }
        catch
        {
            return new List<TaskDto>();
        }
    }

    public async Task<TaskDto?> CreateTaskAsync(CreateTaskDto createTaskDto)
    {
        try
        {
            return await _taskApi.CreateTaskAsync(createTaskDto);
        }
        catch
        {
            return null;
        }
    }

    public async Task<TaskDto?> UpdateTaskAsync(Guid id, UpdateTaskDto updateTaskDto)
    {
        try
        {
            return await _taskApi.UpdateTaskAsync(id, updateTaskDto);
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> DeleteTaskAsync(Guid id)
    {
        try
        {
            await _taskApi.DeleteTaskAsync(id);
            return true;
        }
        catch
        {
            return false;
        }
    }
}