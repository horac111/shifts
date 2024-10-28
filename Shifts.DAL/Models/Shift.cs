using Shifts.DAL.Abstracts;

namespace Shifts.DAL.Models;
public class Shift : Model<long>
{
    public required string Name { get; set; }

    public uint MaxPeople { get; set; }

    public DateOnly Start { get; set; }

    public DateOnly? End { get; set; }

    public virtual ICollection<ShiftDay> ShiftDays { get; set; } = Array.Empty<ShiftDay>();

    public virtual ICollection<Occurence> Occurences { get; set; } = Array.Empty<Occurence>();
}
