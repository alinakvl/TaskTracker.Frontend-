using TaskTracker.Blazor.Domain.DTOs.TaskLists;

namespace TaskTracker.Blazor.Services.Abstraction;

public interface ITaskListService
{
    Task<List<TaskListDto>> GetTaskListsByBoardIdAsync(Guid boardId);
    Task<TaskListDto?> GetTaskListByIdAsync(Guid id);
    Task<TaskListDto?> CreateTaskListAsync(CreateTaskListDto createTaskListDto);
    Task<TaskListDto?> UpdateTaskListAsync(Guid id, UpdateTaskListDto updateTaskListDto);
    Task<bool> DeleteTaskListAsync(Guid id);
  
}