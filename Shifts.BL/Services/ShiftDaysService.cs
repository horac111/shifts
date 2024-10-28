using Shared.Extensions;
using Shifts.BL.Abstract.Services;
using Shifts.DAL.Abstracts.Repository;
using Shifts.DAL.Models;

namespace Shifts.BL.Services;
public class ShiftDaysService : IShiftDaysService
{
    private readonly IShiftDayRepository shiftDayRepository;
    private readonly IShiftRepository shifRepository;

    public ShiftDaysService(IShiftRepository shifRepository, IShiftDayRepository shiftDayRepository)
    {
        this.shifRepository = shifRepository;
        this.shiftDayRepository = shiftDayRepository;
    }

    public async Task CreateMonth(DateOnly month)
    {
        var first = month.GetFirstInMonth();
        var shifts = (await shifRepository.GetAllValidAsync(first)).ToHashSet();

        for (var date = first; date < first.AddMonths(1); date = date.AddDays(1))
        {
            HashSet<Shift> toRemove = new();
            foreach (var shift in shifts)
            {
                if (date < shift.End.GetValueOrMax())
                {
                    ShiftDay day = new()
                    {
                        Shift = shift,
                        Date = date,
                        IsPublished = true
                    };
                    shiftDayRepository.Create(day);
                }
                else
                {
                    toRemove.Add(shift);
                }
            }
            shifts.ExceptWith(toRemove);
        }
    }
}
