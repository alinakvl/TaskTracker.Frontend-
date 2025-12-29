using TaskTracker.Blazor.Domain.DTOs.TaskLists;
using TaskTracker.Blazor.Services.Abstraction;

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
        try
        {
            return await _taskListApi.GetTaskListsByBoardIdAsync(boardId);
        }
        catch
        {
            return new List<TaskListDto>();
        }
    }

    public async Task<TaskListDto?> GetTaskListByIdAsync(Guid id)
    {
        try
        {
            return await _taskListApi.GetTaskListByIdAsync(id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<TaskListDto?> CreateTaskListAsync(CreateTaskListDto createTaskListDto)
    {
        try
        {
            return await _taskListApi.CreateTaskListAsync(createTaskListDto);
        }
        catch
        {
            return null;
        }
    }

    public async Task<TaskListDto?> UpdateTaskListAsync(Guid id, UpdateTaskListDto updateTaskListDto)
    {
        try
        {
            return await _taskListApi.UpdateTaskListAsync(id, updateTaskListDto);
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> DeleteTaskListAsync(Guid id)
    {
        try
        {
            await _taskListApi.DeleteTaskListAsync(id);
            return true;
        }
        catch
        {
            return false;
        }
    }

 
}
