using Shifts.DAL.Abstracts;

namespace Shifts.DAL.Models;
public class ChangeRequest : Model<long>
{
    public string RequestorId { get; set; } = default!;
    public string ReplierId { get; set; } = default!;
    public long ShiftDayId { get; set; }
    public virtual required ApplicationUser Requestor { get; set; }

    public virtual required ApplicationUser Replier { get; set; }

    public virtual required ShiftDay ShiftDay { get; set; }
}
