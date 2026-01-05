using Refit;
using TaskTracker.Blazor.Domain.DTOs.TaskLists;


public interface ITaskListApi
{
    [Get("/tasklists/board/{boardId}")]
    Task<List<TaskListDto>> GetTaskListsByBoardIdAsync(Guid boardId);

    [Get("/tasklists/{id}")]
    Task<TaskListDto> GetTaskListByIdAsync(Guid id);

    [Post("/tasklists")]
    Task<TaskListDto> CreateTaskListAsync([Body] CreateTaskListDto createTaskListDto);

    [Put("/tasklists/{id}")]
    Task<TaskListDto> UpdateTaskListAsync(Guid id, [Body] UpdateTaskListDto updateTaskListDto);

    [Delete("/tasklists/{id}")]
    Task DeleteTaskListAsync(Guid id);


}
