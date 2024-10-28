using Shared.Enums;

namespace Shifts.WebServices.Components.Shared;

public record NavbarItem(string DisplayName, string Route, AplicationRoleEnum? NeededRole = null);
