using Microsoft.AspNetCore.Components;
using MudBlazor;
using TaskTracker.Blazor.Domain.DTOs.Boards;
using TaskTracker.Blazor.Services.Abstraction;
using static MudBlazor.Icons.Custom;

namespace TaskTracker.Blazor.WebApp.Pages.Boards;

public partial class BoardsList
 {
    [Inject]
    private IBoardService BoardService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;

private List<BoardDto> boards = new();
private bool isLoading = true;
private bool showCreateDialog = false;
private bool isCreating = false;
private CreateBoardDto newBoard = new() { BackgroundColor = "#0079BF" };

protected override async Task OnInitializedAsync()
{
    await LoadBoards();
}

private async Task LoadBoards()
{
    isLoading = true;
    boards = await BoardService.GetMyBoardsAsync();
    isLoading = false;
}

private async Task CreateBoard()
{
    if (string.IsNullOrWhiteSpace(newBoard.Title))
    {
        Snackbar.Add("Board title is required", Severity.Warning);
        return;
    }

    isCreating = true;

    var created = await BoardService.CreateBoardAsync(newBoard);

    if (created != null)
    {
        Snackbar.Add("Board created successfully!", Severity.Success);
        showCreateDialog = false;
        newBoard = new() { BackgroundColor = "#0079BF" };
        await LoadBoards();
    }
    else
    {
        Snackbar.Add("Failed to create board", Severity.Error);
    }

    isCreating = false;
}
}

