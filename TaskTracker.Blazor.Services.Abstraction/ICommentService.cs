using TaskTracker.Blazor.Domain.DTOs.Comments;

namespace TaskTracker.Blazor.Services.Abstraction;

public interface ICommentService
{
    Task<List<CommentDto>> GetCommentsByTaskAsync(Guid taskId);
    Task<CommentDto?> CreateCommentAsync(CreateCommentDto createCommentDto);
    Task<bool> DeleteCommentAsync(Guid id);
}