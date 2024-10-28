using Shifts.DAL.Abstracts;

namespace Shifts.DAL.Models;

public class ShiftDay : Model<long>
{
    public DateOnly Date { get; set; }

    public bool IsPublished { get; set; }

    public long ShiftId { get; set; }

    public virtual required Shift Shift { get; set; }

    public virtual ICollection<ShiftAssignment> ShiftAssignments { get; set; } = Array.Empty<ShiftAssignment>();

    public virtual ICollection<WorkingAvailability> WorkingAvailability { get; set; } = Array.Empty<WorkingAvailability>();

    public virtual ICollection<ChangeRequest> ChangeRequests { get; set; } = Array.Empty<ChangeRequest>();
}
