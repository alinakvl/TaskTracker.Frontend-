using TaskTracker.Blazor.Domain.DTOs.Comments;
using TaskTracker.Blazor.Services.Abstraction;
using TaskTracker.Blazor.Services.Abstraction.ExternalApi;

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
        var response = await _commentApi.GetCommentsByTaskAsync(taskId);

        if (response.IsSuccessStatusCode && response.Content != null)
        {
            return response.Content;
        }

        return new List<CommentDto>();
    }

    public async Task<CommentDto?> CreateCommentAsync(CreateCommentDto createCommentDto)
    {
        var response = await _commentApi.CreateCommentAsync(createCommentDto);

        if (response.IsSuccessStatusCode)
        {
            return response.Content;
        }

        return null;
    }

    public async Task<bool> DeleteCommentAsync(Guid id)
    {
        var response = await _commentApi.DeleteCommentAsync(id);
        return response.IsSuccessStatusCode;
    }
}