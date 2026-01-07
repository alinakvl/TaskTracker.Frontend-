using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using MudBlazor;
using TaskTracker.Blazor.Domain.DTOs.Boards;
using TaskTracker.Blazor.Domain.DTOs.TaskLists;
using TaskTracker.Blazor.Services.Abstraction;
using TaskTracker.Blazor.WebApp.Components.Dialogs;

namespace TaskTracker.Blazor.WebApp.Pages.Boards;

public partial class BoardDetails
{
    [Inject]
    private IBoardService BoardService { get; set; } = default!;

    [Inject]
    private ITaskListService TaskListService { get; set; } = default!;

    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;

    [Inject]
    private IJSRuntime JS { get; set; } = default!;

    [Parameter]
    public Guid BoardId { get; set; }

    private BoardDetailDto? board;
    private bool isLoading = true;
    private bool showAddList = false;
    private bool isCreatingList = false;
    private string newListTitle = string.Empty;
    private MudTextField<string>? listTitleField;

    protected override async Task OnInitializedAsync()
    {
        await LoadBoard();
    }

    private async Task LoadBoard()
    {
        try
        {
            isLoading = true;
            board = await BoardService.GetBoardByIdAsync(BoardId);

            if (board == null)
            {
                await JS.InvokeVoidAsync("console.error", "Board not found");
            }
            else
            {
                await JS.InvokeVoidAsync("console.log", $"Board loaded: {board.Title} with {board.TaskLists.Count} lists");
            }
        }
        catch (Exception ex)
        {
            await JS.InvokeVoidAsync("console.error", $"Error loading board: {ex.Message}");
            Snackbar.Add("Failed to load board", Severity.Error);
        }
        finally
        {
            isLoading = false;
        }
    }

    private async Task ShowAddList()
    {
        showAddList = true;
        await Task.Delay(100);
        if (listTitleField != null)
        {
            await listTitleField.FocusAsync();
        }
    }

    private void CancelAddList()
    {
        showAddList = false;
        newListTitle = string.Empty;
    }

    private async Task CreateList()
    {
        if (string.IsNullOrWhiteSpace(newListTitle))
        {
            Snackbar.Add("List title is required", Severity.Warning);
            return;
        }

        try
        {
            isCreatingList = true;

            var createDto = new CreateTaskListDto
            {
                Title = newListTitle.Trim(),
                BoardId = BoardId
            };

            await JS.InvokeVoidAsync("console.log", "Creating list:", createDto.Title);

            var created = await TaskListService.CreateTaskListAsync(createDto);

            if (created != null)
            {
                await JS.InvokeVoidAsync("console.log", "List created:", created.Id);

                Snackbar.Add($"List '{created.Title}' created!", Severity.Success);
                newListTitle = string.Empty;
                showAddList = false;

                await LoadBoard();
            }
            else
            {
                await JS.InvokeVoidAsync("console.error", "List creation returned null");
                Snackbar.Add("Failed to create list", Severity.Error);
            }
        }
        catch (Exception ex)
        {
            await JS.InvokeVoidAsync("console.error", "Error creating list:", ex.Message);
            Snackbar.Add($"Error: {ex.Message}", Severity.Error);
        }
        finally
        {
            isCreatingList = false;
        }
    }

    private async Task HandleListKeyDown(KeyboardEventArgs e)
    {
        if (e.Key == "Enter" && !string.IsNullOrWhiteSpace(newListTitle))
        {
            await CreateList();
        }
        else if (e.Key == "Escape")
        {
            CancelAddList();
        }
    }

    private async Task EditBoard()
    {
        if (board == null) return;

        var boardDto = new BoardDto
        {
            Id = board.Id,
            Title = board.Title,
            Description = board.Description,
            BackgroundColor = board.BackgroundColor,
            OwnerId = board.OwnerId
        };

        var parameters = new DialogParameters<EditBoardDialog>
        {
            { x => x.Board, boardDto }
        };

        var dialog = await DialogService.ShowAsync<EditBoardDialog>("Edit Board", parameters);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            await LoadBoard();
        }
    }

    private async Task ShowMembers()
    {
        if (board == null) return;

        var boardDto = new BoardDto
        {
            Id = board.Id,
            Title = board.Title,
            OwnerId = board.OwnerId,
            MembersCount = board.Members?.Count ?? 0
        };

        var parameters = new DialogParameters<BoardMembersDialog>
        {
            { x => x.Board, boardDto }
        };

        await DialogService.ShowAsync<BoardMembersDialog>("Board Members", parameters, new DialogOptions
        {
            MaxWidth = MaxWidth.Medium,
            FullWidth = true
        });
    }
}