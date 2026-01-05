using Refit;
using TaskTracker.Blazor.Domain.DTOs.TaskLists;


public interface ITaskListApi
{
    [Get("/tasklists/board/{boardId}")]
    [Headers("Authorization: Bearer")]
    Task<List<TaskListDto>> GetTaskListsByBoardIdAsync(Guid boardId);

    [Get("/tasklists/{id}")]
    [Headers("Authorization: Bearer")]
    Task<TaskListDto> GetTaskListByIdAsync(Guid id);

    [Post("/tasklists")]
    [Headers("Authorization: Bearer")]
    Task<TaskListDto> CreateTaskListAsync([Body] CreateTaskListDto createTaskListDto);

    [Put("/tasklists/{id}")]
    [Headers("Authorization: Bearer")]
    Task<TaskListDto> UpdateTaskListAsync(Guid id, [Body] UpdateTaskListDto updateTaskListDto);

    [Delete("/tasklists/{id}")]
    [Headers("Authorization: Bearer")]
    Task DeleteTaskListAsync(Guid id);


}
