using Shifts.DAL.Abstracts;

namespace Shifts.DAL.Models;

//[Index(nameof(User), nameof(ShiftDay), IsUnique = true)]
public class ShiftAssignment : Model<long>
{
    public long ShiftDayId { get; set; }

    public string UserId { get; set; }

    public required virtual ApplicationUser User { get; set; }

    public required virtual ShiftDay ShiftDay { get; set; }

    public bool Forced { get; set; }
}
