using TaskTracker.Blazor.Domain.DTOs.Comments;
using TaskTracker.Blazor.Services.Abstraction;

namespace TaskTracker.Blazor.Services;

public class CommentService : ICommentService
{
    private readonly ICommentApi _commentApi;

    public CommentService(ICommentApi commentApi)
    {
        _commentApi = commentApi;
    }

    public async Task<List<CommentDto>> GetCommentsByTaskAsync(Guid taskId)
    {
        try
        {
            return await _commentApi.GetCommentsByTaskAsync(taskId);
        }
        catch
        {
            return new List<CommentDto>();
        }
    }

    public async Task<CommentDto?> CreateCommentAsync(CreateCommentDto createCommentDto)
    {
        try
        {
            return await _commentApi.CreateCommentAsync(createCommentDto);
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> DeleteCommentAsync(Guid id)
    {
        try
        {
            await _commentApi.DeleteCommentAsync(id);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
