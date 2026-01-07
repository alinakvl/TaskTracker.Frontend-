using TaskTracker.Blazor.Domain.DTOs.Boards;
using TaskTracker.Blazor.Services.Abstraction;
using TaskTracker.Blazor.Services.Abstraction.ExternalApi;

namespace TaskTracker.Blazor.Services;

public class BoardService : IBoardService
{
    private readonly IBoardApi _boardApi;

    public BoardService(IBoardApi boardApi)
    {
        _boardApi = boardApi;
    }

    public async Task<List<BoardDto>> GetMyBoardsAsync()
    {
        var response = await _boardApi.GetMyBoardsAsync();

        if (response.IsSuccessStatusCode && response.Content != null)
        {
            return response.Content;
        }

        return new List<BoardDto>();
    }

    public async Task<BoardDetailDto?> GetBoardByIdAsync(Guid id)
    {
        var response = await _boardApi.GetBoardByIdAsync(id);

        if (response.IsSuccessStatusCode)
        {
            return response.Content;
        }

        return null;
    }

    public async Task<List<BoardDto>> GetArchivedBoardsAsync()
    {
        var response = await _boardApi.GetArchivedBoardsAsync();

        if (response.IsSuccessStatusCode && response.Content != null)
        {
            return response.Content;
        }

        return new List<BoardDto>();
    }

    public async Task<BoardDto?> CreateBoardAsync(CreateBoardDto createBoardDto)
    {
        var response = await _boardApi.CreateBoardAsync(createBoardDto);

        if (response.IsSuccessStatusCode)
        {
            return response.Content;
        }

        return null;
    }

    public async Task<BoardDto?> UpdateBoardAsync(Guid id, UpdateBoardDto updateBoardDto)
    {
        var response = await _boardApi.UpdateBoardAsync(id, updateBoardDto);

        if (response.IsSuccessStatusCode)
        {
            return response.Content;
        }

        return null;
    }

    public async Task<bool> DeleteBoardAsync(Guid id)
    {
        var response = await _boardApi.DeleteBoardAsync(id);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ArchiveBoardAsync(Guid id)
    {
        var response = await _boardApi.ArchiveBoardAsync(id);
        return response.IsSuccessStatusCode;
    }

    public async Task<List<BoardMemberDto>> GetBoardMembersAsync(Guid boardId)
    {
        var response = await _boardApi.GetBoardMembersAsync(boardId);

        if (response.IsSuccessStatusCode && response.Content != null)
        {
            return response.Content;
        }

        return new List<BoardMemberDto>();
    }

    public async Task<bool> AddBoardMemberAsync(Guid boardId, AddBoardMemberDto addMemberDto)
    {
        var response = await _boardApi.AddBoardMemberAsync(boardId, addMemberDto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RemoveBoardMemberAsync(Guid boardId, Guid userId)
    {
        var response = await _boardApi.RemoveBoardMemberAsync(boardId, userId);
        return response.IsSuccessStatusCode;
    }
}
