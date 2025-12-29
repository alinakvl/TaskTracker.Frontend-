using TaskTracker.Blazor.Domain.DTOs.Boards;
using TaskTracker.Blazor.Services.Abstraction;

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
        try
        {
            return await _boardApi.GetMyBoardsAsync();
        }
        catch
        {
            return new List<BoardDto>();
        }
    }

    public async Task<BoardDetailDto?> GetBoardByIdAsync(Guid id)
    {
        try
        {
            return await _boardApi.GetBoardByIdAsync(id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<BoardDto>> GetArchivedBoardsAsync()
    {
        try
        {
            return await _boardApi.GetArchivedBoardsAsync();
        }
        catch
        {
            return new List<BoardDto>();
        }
    }

    public async Task<BoardDto?> CreateBoardAsync(CreateBoardDto createBoardDto)
    {
        try
        {
            return await _boardApi.CreateBoardAsync(createBoardDto);
        }
        catch
        {
            return null;
        }
    }

    public async Task<BoardDto?> UpdateBoardAsync(Guid id, UpdateBoardDto updateBoardDto)
    {
        try
        {
            return await _boardApi.UpdateBoardAsync(id, updateBoardDto);
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> DeleteBoardAsync(Guid id)
    {
        try
        {
            await _boardApi.DeleteBoardAsync(id);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> ArchiveBoardAsync(Guid id)
    {
        try
        {
            await _boardApi.ArchiveBoardAsync(id);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public async Task<List<BoardMemberDto>> GetBoardMembersAsync(Guid boardId)
    {
        try
        {
            return await _boardApi.GetBoardMembersAsync(boardId);
        }
        catch
        {
            return new List<BoardMemberDto>();
        }
    }

    public async Task<bool> AddBoardMemberAsync(Guid boardId, AddBoardMemberDto addMemberDto)
    {
        try
        {
            await _boardApi.AddBoardMemberAsync(boardId, addMemberDto);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> RemoveBoardMemberAsync(Guid boardId, Guid userId)
    {
        try
        {
            await _boardApi.RemoveBoardMemberAsync(boardId, userId);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
