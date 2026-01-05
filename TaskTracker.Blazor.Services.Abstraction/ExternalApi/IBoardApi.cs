using Refit;
using TaskTracker.Blazor.Domain.DTOs.Boards;

namespace TaskTracker.Blazor.Services.Abstraction.ExternalApi;

public interface IBoardApi
{
    [Get("/boards/my")]
    Task<List<BoardDto>> GetMyBoardsAsync();

    [Get("/boards/{id}")]
    Task<BoardDetailDto> GetBoardByIdAsync(Guid id);

    [Get("/boards/archived")]
    Task<List<BoardDto>> GetArchivedBoardsAsync();

    [Post("/boards")]
    Task<BoardDto> CreateBoardAsync([Body] CreateBoardDto createBoardDto);

    [Put("/boards/{id}")]
    Task<BoardDto> UpdateBoardAsync(Guid id, [Body] UpdateBoardDto updateBoardDto);

    [Delete("/boards/{id}")]
    Task DeleteBoardAsync(Guid id);

    [Patch("/boards/{id}/archive")]
    Task ArchiveBoardAsync(Guid id);

    [Get("/boards/{boardId}/members")]
    Task<List<BoardMemberDto>> GetBoardMembersAsync(Guid boardId);

    [Post("/boards/{boardId}/members")]
    Task AddBoardMemberAsync(Guid boardId, [Body] AddBoardMemberDto addMemberDto);

    [Delete("/boards/{boardId}/members/{userId}")]
    Task RemoveBoardMemberAsync(Guid boardId, Guid userId);
}