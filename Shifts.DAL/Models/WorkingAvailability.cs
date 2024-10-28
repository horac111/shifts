using Shared.Enums;
using Shifts.DAL.Abstracts;

namespace Shifts.DAL.Models;

public class WorkingAvailability : Model<long>
{
    public AvailabilityState State { get; set; }

    public long ShiftDayId { get; set; }

    public string UserId { get; set; } = default!;

    public virtual required ShiftDay ShiftDay { get; set; }

    public virtual required ApplicationUser User { get; set; }
}
