using Microsoft.AspNetCore.Components;
using MudBlazor;
using TaskTracker.Blazor.Domain.DTOs.Boards;
using TaskTracker.Blazor.Services.Abstraction;

namespace TaskTracker.Blazor.WebApp.Components.Shared;

public partial class BoardCard
{
    [Inject]
    private IBoardService BoardService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;

    [Parameter] public BoardDto Board { get; set; } = null!;
    [Parameter] public EventCallback OnDeleted { get; set; }

    private bool showEditDialog = false;
    private bool showMembersDialog = false;

    private void EditBoard() => showEditDialog = true;
    private void ManageMembers() => showMembersDialog = true;

    private async Task HandleEditClose(BoardDto? updatedBoard)
    {
        showEditDialog = false;
        if (updatedBoard != null)
        {
            Board.Title = updatedBoard.Title;
            Board.Description = updatedBoard.Description;
            Board.BackgroundColor = updatedBoard.BackgroundColor;

            await OnDeleted.InvokeAsync();
            StateHasChanged();
        }
    }

    private void HandleMembersClose()
    {
        showMembersDialog = false;
    }

    private async Task ArchiveBoard()
    {
        var success = await BoardService.ArchiveBoardAsync(Board.Id);
        if (success)
        {
            Snackbar.Add("Board archived", Severity.Success);
            await OnDeleted.InvokeAsync();
        }
        else
        {
            Snackbar.Add("Failed to archive board", Severity.Error);
        }
    }

    private async Task DeleteBoard()
    {
        bool? result = await DialogService.ShowMessageBox(
            "Confirm Delete",
            $"Are you sure you want to delete '{Board.Title}'? This action cannot be undone.",
            yesText: "Delete", cancelText: "Cancel");

        if (result == true)
        {
            var success = await BoardService.DeleteBoardAsync(Board.Id);
            if (success)
            {
                Snackbar.Add("Board deleted", Severity.Success);
                await OnDeleted.InvokeAsync();
            }
            else
            {
                Snackbar.Add("Failed to delete board", Severity.Error);
            }
        }
    }
}