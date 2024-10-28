using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Shared.Texts;
using Shifts.BL.Abstract.Services;

namespace Shifts.WebServices.Components.Pages.Account;

public partial class Login
{
    [Inject]
    private IManageUsersService manageUserService { get; init; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; init; } = default!;

    [Inject]
    private IToastService toastService { get; init; } = default!;

    private string? Username { get; set; }

    private string? Password { get; set; }

    private bool ShowPassword { get; set; }

    private bool SigningIn { get; set; }

    [SupplyParameterFromQuery]
    private string? ReturnUrl { get; set; }

    private async Task LoginUser()
    {
        if (string.IsNullOrEmpty(Username))
        {
            toastService.ShowError(Errors.E0004);
            return;
        }

        if (string.IsNullOrEmpty(Password))
        {
            toastService.ShowError(Errors.E0005);
            return;
        }

        SigningIn = true;

        var result = await manageUserService.SignIn(Username, Password);

        SigningIn = false;

        if (string.IsNullOrEmpty(result))
        {
            NavigationManager.NavigateTo(ReturnUrl ?? NavigationManager.BaseUri);
        }
        else
        {
            toastService.ShowError(result);
        }

    }

    private void OnShowPasswordClick()
    {
        ShowPassword ^= true;
    }
}
