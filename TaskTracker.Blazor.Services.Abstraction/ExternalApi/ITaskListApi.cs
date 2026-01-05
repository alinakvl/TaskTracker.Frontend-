using Refit;
using TaskTracker.Blazor.Domain.DTOs.TaskLists;

namespace TaskTracker.Blazor.Services.Abstraction.ExternalApi;

public interface ITaskListApi
{
    [Get("/tasklists/board/{boardId}")]
    Task<IApiResponse<List<TaskListDto>>> GetTaskListsByBoardIdAsync(Guid boardId);

    [Get("/tasklists/{id}")]
    Task<IApiResponse<TaskListDto>> GetTaskListByIdAsync(Guid id);

    [Post("/tasklists")]
    Task<IApiResponse<TaskListDto>> CreateTaskListAsync([Body] CreateTaskListDto createTaskListDto);

    [Put("/tasklists/{id}")]
    Task<IApiResponse<TaskListDto>> UpdateTaskListAsync(Guid id, [Body] UpdateTaskListDto updateTaskListDto);

    [Delete("/tasklists/{id}")]
    Task<IApiResponse> DeleteTaskListAsync(Guid id);
}
