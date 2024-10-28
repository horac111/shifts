using Shifts.BL.Data.Records;
using Shifts.DAL.Models;

namespace Shifts.BL.Data.Other;
public class UserUsage
{
    public required ApplicationUser User { get; set; }

    public int Count => WorkingDays.Count;

    public List<WorkingDay> WorkingDays { get; set; } = new List<WorkingDay>();
}
