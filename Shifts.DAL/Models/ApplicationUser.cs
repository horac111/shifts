using Microsoft.AspNetCore.Identity;
using Shifts.DAL.Abstracts;

namespace Shifts.DAL.Models;

public class ApplicationUser : IdentityUser, IModel<string>
{
    public required string Name { get; set; }

    public required string Surname { get; set; }

    public ICollection<ShiftAssignment> ShiftAssignments { get; set; } = Array.Empty<ShiftAssignment>();
    public ICollection<WorkingAvailability> WorkingAvailability { get; set; } = Array.Empty<WorkingAvailability>();
    public ICollection<ChangeRequest> MyRequests { get; set; } = Array.Empty<ChangeRequest>();
    public ICollection<ChangeRequest> RequestsForMe { get; set; } = Array.Empty<ChangeRequest>();
}
