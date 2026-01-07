using TaskTracker.Blazor.Domain.DTOs.TaskLists;
using TaskTracker.Blazor.Services.Abstraction;
using TaskTracker.Blazor.Services.Abstraction.ExternalApi;

namespace TaskTracker.Blazor.Services;

public class TaskListService : ITaskListService
{
    private readonly ITaskListApi _taskListApi;

    public TaskListService(ITaskListApi taskListApi)
    {
        _taskListApi = taskListApi;
    }

    public async Task<List<TaskListDto>> GetTaskListsByBoardIdAsync(Guid boardId)
    {
        var response = await _taskListApi.GetTaskListsByBoardIdAsync(boardId);

        if (response.IsSuccessStatusCode && response.Content != null)
        {
            return response.Content;
        }

        return new List<TaskListDto>();
    }

    public async Task<TaskListDto?> GetTaskListByIdAsync(Guid id)
    {
        var response = await _taskListApi.GetTaskListByIdAsync(id);

        if (response.IsSuccessStatusCode)
        {
            return response.Content;
        }

        return null;
    }

    public async Task<TaskListDto?> CreateTaskListAsync(CreateTaskListDto createTaskListDto)
    {
        var response = await _taskListApi.CreateTaskListAsync(createTaskListDto);

        if (response.IsSuccessStatusCode)
        {
            return response.Content;
        }

        return null;
    }

    public async Task<TaskListDto?> UpdateTaskListAsync(Guid id, UpdateTaskListDto updateTaskListDto)
    {
        var response = await _taskListApi.UpdateTaskListAsync(id, updateTaskListDto);

        if (response.IsSuccessStatusCode)
        {
            return response.Content;
        }

        return null;
    }

    public async Task<bool> DeleteTaskListAsync(Guid id)
    {
        var response = await _taskListApi.DeleteTaskListAsync(id);
        return response.IsSuccessStatusCode;
    }
}