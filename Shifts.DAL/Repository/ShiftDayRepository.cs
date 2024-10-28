using Microsoft.EntityFrameworkCore;
using Shifts.DAL.Abstracts.Repository;
using Shifts.DAL.Models;

namespace Shifts.DAL.Repository;
public class ShiftDayRepository : RepositoryBase<ShiftDay, long>, IShiftDayRepository
{
    public ShiftDayRepository(ApplicationDbContext ctx) : base(ctx)
    {
    }

    public async Task<IEnumerable<ShiftDay>> GetAllByShiftAndDurationAsync(Shift shift, DateOnly from, DateOnly to)
    {
        return await Context.ShiftDays.Where(x => x.Shift == shift && from <= x.Date && to > x.Date)
            .Include(x => x.WorkingAvailability)
            .OrderBy(x => x.WorkingAvailability.Count())
            .ToArrayAsync();
    }
}
