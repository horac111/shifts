using Shifts.DAL.Abstracts;

namespace Shifts.DAL.Models;
public class Occurence : Model<long>
{
    public DateOnly Date { get; set; }

    public required string Name { get; set; }

    public int ChangeOfPeople { get; set; }

    public long ShiftId { get; set; }

    public virtual required Shift Shift { get; set; }
}
