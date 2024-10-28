using Shared.Enums;
using Shifts.BL.Abstract;

namespace Shifts.BL.Data.DTO;
public class UserDto : IDto
{
    public required string UserName { get; init; }

    public required string Name { get; init; }

    public required string SurName { get; init; }

    public required string Email { get; init; }

    public List<AplicationRoleEnum> Roles { get; init; } = new List<AplicationRoleEnum>();
}
