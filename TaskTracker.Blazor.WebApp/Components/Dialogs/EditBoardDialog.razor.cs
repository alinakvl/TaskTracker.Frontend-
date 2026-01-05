using Microsoft.AspNetCore.Components;
using MudBlazor;
using TaskTracker.Blazor.Domain.DTOs.Boards;
using TaskTracker.Blazor.Services.Abstraction;

namespace TaskTracker.Blazor.WebApp.Components.Dialogs;

public partial class EditBoardDialog 
{
    [Inject]
    private IBoardService BoardService { get; set; } = default!;
    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;
    [Parameter] public BoardDto Board { get; set; } = null!;
    [Parameter] public EventCallback<BoardDto?> OnClose { get; set; }
private UpdateBoardDto updateBoard = new();
private bool isSaving = false;

protected override void OnInitialized()
{
    updateBoard = new UpdateBoardDto
    {
        Title = Board.Title,
        Description = Board.Description,
        BackgroundColor = Board.BackgroundColor
    };
}

private async Task SaveChanges()
{
    if (string.IsNullOrWhiteSpace(updateBoard.Title))
    {
        Snackbar.Add("Board title is required", Severity.Warning);
        return;
    }
    isSaving = true;
    try
    {
        var updated = await BoardService.UpdateBoardAsync(Board.Id, updateBoard);
        if (updated != null)
        {
            Snackbar.Add("Board updated successfully!", Severity.Success);
            await OnClose.InvokeAsync(updated);
        }
    }
    catch
    {
        Snackbar.Add("Failed to update board", Severity.Error);
    }
    finally
    {
        isSaving = false;
    }
}
private Task Cancel() => OnClose.InvokeAsync(null);
}

