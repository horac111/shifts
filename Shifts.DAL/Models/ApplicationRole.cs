using Microsoft.AspNetCore.Identity;
using Shared.Enums;
using Shifts.DAL.Abstracts;

namespace Shifts.DAL.Models;
public class ApplicationRole : IdentityRole<string>, IModel<string>
{
    public required AplicationRoleEnum Role { get; set; }

    public override string? Name { get => Role.ToString(); set => throw new NotImplementedException(); }
    public override string? NormalizedName { get => Role.ToString(); set => throw new NotImplementedException(); }
}
