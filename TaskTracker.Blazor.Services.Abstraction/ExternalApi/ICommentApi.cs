using Refit;
using TaskTracker.Blazor.Domain.DTOs.Comments;

namespace TaskTracker.Blazor.Services.Abstraction.ExternalApi;

public interface ICommentApi
{
    [Get("/comments/task/{taskId}")]
    Task<List<CommentDto>> GetCommentsByTaskAsync(Guid taskId);

    [Post("/comments")]
    Task<CommentDto> CreateCommentAsync([Body] CreateCommentDto createCommentDto);

    [Delete("/comments/{id}")]
    Task DeleteCommentAsync(Guid id);
}