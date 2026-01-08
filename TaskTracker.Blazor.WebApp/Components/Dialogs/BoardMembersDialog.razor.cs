using Microsoft.AspNetCore.Components;
using MudBlazor;
using TaskTracker.Blazor.Domain.DTOs.Boards;
using TaskTracker.Blazor.Domain.DTOs.Users;
using TaskTracker.Blazor.Services.Abstraction;

namespace TaskTracker.Blazor.WebApp.Components.Dialogs;

public partial class BoardMembersDialog
{
    [Inject]
    private IBoardService BoardService { get; set; } = default!;

    [Inject]
    private IUserService UserService { get; set; } = default!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;

    [Inject]
    private IAuthService AuthService { get; set; } = default!;

    [Parameter] public BoardDto Board { get; set; } = null!;
    [Parameter] public EventCallback OnClose { get; set; }

    private List<BoardMemberDto> members = new();

    private int myRole = 3; 
    private Guid currentUserId;
   

    private bool isLoading = true;
    private bool isAdding = false;

    private UserDto? selectedUserToAdd;
    private int selectedRole = 3;

    protected override async Task OnInitializedAsync()
    {
        await LoadMembers();
    }

    private async Task LoadMembers()
    {
        isLoading = true;
        try
        {
            members = await BoardService.GetBoardMembersAsync(Board.Id);

            var currentUser = await AuthService.GetCurrentUserAsync();
            if (currentUser != null)
            {
                currentUserId = currentUser.Id;

                var me = members.FirstOrDefault(m => m.UserId == currentUserId);
                if (me != null)
                {
                    myRole = me.Role; 
                }
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add($"Error: {ex.Message}", Severity.Error);
        }
        finally
        {
            isLoading = false;
        }
    }

    private async Task<IEnumerable<UserDto>> SearchUsers(string value, CancellationToken token)
    {
        if (string.IsNullOrEmpty(value))
            return new List<UserDto>();

        try
        {
            return await UserService.SearchUsersAsync(value);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Search error: {ex.Message}");
            return new List<UserDto>();
        }
    }

    private async Task AddSelectedMember()
    {
        if (selectedUserToAdd == null)
        {
            Snackbar.Add("Please select a user first", Severity.Warning);
            return;
        }

        if (members.Any(m => m.UserId == selectedUserToAdd.Id))
        {
            Snackbar.Add("User is already a member of this board", Severity.Info);
            return;
        }

        isAdding = true;
        try
        {
            var success = await BoardService.AddBoardMemberAsync(Board.Id,
                new AddBoardMemberDto
                {
                    UserId = selectedUserToAdd.Id,
                    Role = selectedRole
                });

            if (success)
            {
                Snackbar.Add($"{selectedUserToAdd.FirstName} added!", Severity.Success);
                selectedUserToAdd = null;
                await LoadMembers();
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add($"Error: {ex.Message}", Severity.Error);
        }
        finally
        {
            isAdding = false;
        }
    }

    private async Task RemoveMember(BoardMemberDto member)
    {
        try
        {
            if (await BoardService.RemoveBoardMemberAsync(Board.Id, member.UserId))
            {
                Snackbar.Add("Member removed", Severity.Success);
                await LoadMembers();
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    private Task Close() => OnClose.InvokeAsync();
}