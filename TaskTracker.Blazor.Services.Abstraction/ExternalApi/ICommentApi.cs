using Refit;
using TaskTracker.Blazor.Domain.DTOs.Comments;

namespace TaskTracker.Blazor.Services.Abstraction.ExternalApi;

public interface ICommentApi
{
    [Get("/comments/task/{taskId}")]
    Task<IApiResponse<List<CommentDto>>> GetCommentsByTaskAsync(Guid taskId);

    [Post("/comments")]
    Task<IApiResponse<CommentDto>> CreateCommentAsync([Body] CreateCommentDto createCommentDto);

    [Delete("/comments/{id}")]
    Task<IApiResponse> DeleteCommentAsync(Guid id);
}