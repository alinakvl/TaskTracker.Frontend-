using Refit;
using TaskTracker.Blazor.Domain.DTOs.Boards;

namespace TaskTracker.Blazor.Services.Abstraction;

public interface IBoardApi
{
    [Get("/boards/my")]
    [Headers("Authorization: Bearer")]
    Task<List<BoardDto>> GetMyBoardsAsync();

    [Get("/boards/{id}")]
    [Headers("Authorization: Bearer")]
    Task<BoardDetailDto> GetBoardByIdAsync(Guid id);

    [Get("/boards/archived")]
    [Headers("Authorization: Bearer")]
    Task<List<BoardDto>> GetArchivedBoardsAsync();

    [Post("/boards")]
    [Headers("Authorization: Bearer")]
    Task<BoardDto> CreateBoardAsync([Body] CreateBoardDto createBoardDto);

    [Put("/boards/{id}")]
    [Headers("Authorization: Bearer")]
    Task<BoardDto> UpdateBoardAsync(Guid id, [Body] UpdateBoardDto updateBoardDto);

    [Delete("/boards/{id}")]
    [Headers("Authorization: Bearer")]
    Task DeleteBoardAsync(Guid id);

    [Patch("/boards/{id}/archive")]
    [Headers("Authorization: Bearer")]
    Task ArchiveBoardAsync(Guid id);

    [Get("/boards/{boardId}/members")]
    [Headers("Authorization: Bearer")]
    Task<List<BoardMemberDto>> GetBoardMembersAsync(Guid boardId);

    [Post("/boards/{boardId}/members")]
    [Headers("Authorization: Bearer")]
    Task AddBoardMemberAsync(Guid boardId, [Body] AddBoardMemberDto addMemberDto);

    [Delete("/boards/{boardId}/members/{userId}")]
    [Headers("Authorization: Bearer")]
    Task RemoveBoardMemberAsync(Guid boardId, Guid userId);
}