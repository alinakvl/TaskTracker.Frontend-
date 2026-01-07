using TaskTracker.Blazor.Domain.DTOs.Boards;

namespace TaskTracker.Blazor.Services.Abstraction;

public interface IBoardService
{
    Task<List<BoardDto>> GetMyBoardsAsync();
    Task<BoardDetailDto?> GetBoardByIdAsync(Guid id);
    Task<List<BoardDto>> GetArchivedBoardsAsync();
    Task<BoardDto?> CreateBoardAsync(CreateBoardDto createBoardDto);
    Task<BoardDto?> UpdateBoardAsync(Guid id, UpdateBoardDto updateBoardDto);
    Task<bool> DeleteBoardAsync(Guid id);
    Task<bool> ArchiveBoardAsync(Guid id);
    Task<List<BoardMemberDto>> GetBoardMembersAsync(Guid boardId);
    Task<bool> AddBoardMemberAsync(Guid boardId, AddBoardMemberDto addMemberDto);
    Task<bool> RemoveBoardMemberAsync(Guid boardId, Guid userId);
}
