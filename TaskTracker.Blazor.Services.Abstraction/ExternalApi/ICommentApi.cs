using Refit;
using TaskTracker.Blazor.Domain.DTOs.Comments;

namespace TaskTracker.Blazor.Services.Abstraction.ExternalApi;

public interface ICommentApi
{
    [Get("/comments/task/{taskId}")]
    [Headers("Authorization: Bearer")]
    Task<List<CommentDto>> GetCommentsByTaskAsync(Guid taskId);

    [Post("/comments")]
    [Headers("Authorization: Bearer")]
    Task<CommentDto> CreateCommentAsync([Body] CreateCommentDto createCommentDto);

    [Delete("/comments/{id}")]
    [Headers("Authorization: Bearer")]
    Task DeleteCommentAsync(Guid id);
}