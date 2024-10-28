using Shared.Enums;
using System.Collections.ObjectModel;

namespace Shifts.WebServices.Components.Shared;

public class NavbarItemProvider
{
    private readonly IReadOnlyCollection<NavbarItem> items = new ReadOnlyCollection<NavbarItem>(new List<NavbarItem>
    {
        new("Vytvoř uživatele", "Create-User", AplicationRoleEnum.CreateUser),
        new("Vytvoř směny", "Create-shifts", AplicationRoleEnum.CreateShifts),
        new("Zapiš si směny", "Create-Availability", AplicationRoleEnum.CreateAvailability),
        new("Spravuj měsíc", "Manage-Month", AplicationRoleEnum.ManageMonth),
        new("Přihlaš se", "Account/Log-In"),
    });

    public IEnumerable<NavbarItem> GetItems() => items;
}
