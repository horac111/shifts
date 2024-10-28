using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Shifts.WebServices.Components.Shared;

public partial class NavBar
{
    private DesignThemeModes Mode { get; set; }
    private OfficeColor? OfficeColor { get; set; }

    [Inject]
    private NavbarItemProvider NavbarItemProvider { get; init; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; init; } = default!;

    [Inject]
    private GlobalState GlobalState { get; init; } = default!;

    private string? Url { get; set; }

    private bool DarkMode { get; set; }

    bool firstTime = true;

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            GlobalState.OnChange += () => DarkMode = GlobalState.Luminance is StandardLuminance.DarkMode;

            Url = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
            StateHasChanged();
        }

        base.OnAfterRender(firstRender);
    }

    private void OnClicked(MouseEventArgs e)
    {
        DarkMode ^= true;
        Mode = DarkMode ? DesignThemeModes.Dark : DesignThemeModes.Light;
    }

    private void OnLumienceChanged(LuminanceChangedEventArgs e)
    {
        if (firstTime)
        {
            DarkMode = e.IsDark;
            firstTime = false;
        }
    }

    private void OnNavbarClicked(string? url = null)
    {
        Url = url;
        NavigationManager.NavigateTo(url ?? NavigationManager.BaseUri);
    }
}
