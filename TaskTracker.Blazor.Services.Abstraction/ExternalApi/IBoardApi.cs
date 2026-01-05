using Refit;
using TaskTracker.Blazor.Domain.DTOs.Boards;

namespace TaskTracker.Blazor.Services.Abstraction.ExternalApi;

public interface IBoardApi
{
    [Get("/boards/my")]
    Task<IApiResponse<List<BoardDto>>> GetMyBoardsAsync();

    [Get("/boards/{id}")]
    Task<IApiResponse<BoardDetailDto>> GetBoardByIdAsync(Guid id);

    [Get("/boards/archived")]
    Task<IApiResponse<List<BoardDto>>> GetArchivedBoardsAsync();

    [Post("/boards")]
    Task<IApiResponse<BoardDto>> CreateBoardAsync([Body] CreateBoardDto createBoardDto);

    [Put("/boards/{id}")]
    Task<IApiResponse<BoardDto>> UpdateBoardAsync(Guid id, [Body] UpdateBoardDto updateBoardDto);

    [Delete("/boards/{id}")]
    Task<IApiResponse> DeleteBoardAsync(Guid id); 

    [Patch("/boards/{id}/archive")]
    Task<IApiResponse> ArchiveBoardAsync(Guid id);

    [Get("/boards/{boardId}/members")]
    Task<IApiResponse<List<BoardMemberDto>>> GetBoardMembersAsync(Guid boardId);

    [Post("/boards/{boardId}/members")]
    Task<IApiResponse> AddBoardMemberAsync(Guid boardId, [Body] AddBoardMemberDto addMemberDto);

    [Delete("/boards/{boardId}/members/{userId}")]
    Task<IApiResponse> RemoveBoardMemberAsync(Guid boardId, Guid userId);
}