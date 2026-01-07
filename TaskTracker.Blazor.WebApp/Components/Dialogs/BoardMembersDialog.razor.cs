using Microsoft.AspNetCore.Components;
using MudBlazor;
using TaskTracker.Blazor.Domain.DTOs.Boards;
using TaskTracker.Blazor.Services.Abstraction;

namespace TaskTracker.Blazor.WebApp.Components.Dialogs;

public partial class BoardMembersDialog
{
    [Inject]
    private IBoardService BoardService { get; set; } = default!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;

    [Parameter] public BoardDto Board { get; set; } = null!;
    [Parameter] public EventCallback OnClose { get; set; }

    private List<BoardMemberDto> members = new();
    private bool isLoading = true;
    private bool isAdding = false;
    private string newUserIdString = string.Empty;
    private int selectedRole = 0;

    protected override async Task OnInitializedAsync() => await LoadMembers();

    private async Task LoadMembers()
    {
        isLoading = true;
        try
        {
            members = await BoardService.GetBoardMembersAsync(Board.Id);
        }
        finally
        {
            isLoading = false;
        }
    }

    private async Task AddMemberById()
    {
        if (!Guid.TryParse(newUserIdString, out var userGuid))
        {
            Snackbar.Add("Invalid GUID format", Severity.Warning);
            return;
        }

        isAdding = true;
        try
        {
            var success = await BoardService.AddBoardMemberAsync(Board.Id,
                new AddBoardMemberDto { UserId = userGuid, Role = selectedRole });

            if (success)
            {
                Snackbar.Add("Member added!", Severity.Success);
                newUserIdString = "";
                await LoadMembers();
            }
        }
        finally
        {
            isAdding = false;
        }
    }

    private async Task RemoveMember(BoardMemberDto member)
    {
        if (await BoardService.RemoveBoardMemberAsync(Board.Id, member.UserId))
        {
            Snackbar.Add("Member removed", Severity.Success);
            await LoadMembers();
        }
    }

    private Task Close() => OnClose.InvokeAsync();
}