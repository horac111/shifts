using Microsoft.EntityFrameworkCore;
using Shared.Enums;
using Shifts.DAL.Abstracts.Repository;
using Shifts.DAL.Models;

namespace Shifts.DAL.Repository;
public class WorkingAvailabilityRepository : RepositoryBase<WorkingAvailability, long>, IWorkingAvailabilityRepository
{
    public WorkingAvailabilityRepository(ApplicationDbContext ctx) : base(ctx)
    {
    }

    public async Task<IEnumerable<ApplicationUser>> GetAvailableUsersByShiftAndDurationAsync(Shift shift, DateOnly from, DateOnly to)
    {
        return await GetByShiftAndDuration(shift, from, to)
            .Select(x => x.User)
            .Distinct()
            .OrderBy(x => x.ShiftAssignments.Count)
            .ThenBy(x => x.Surname)
            .ThenBy(x => x.Name)
            .ToArrayAsync();
    }

    public async Task<Dictionary<DateOnly, WorkingAvailability[]>> GetWorkingAvailabilitiesByShiftAndDurationAsync(Shift shift, DateOnly from, DateOnly to)
    {
        return await GetByShiftAndDuration(shift, from, to)
            .Where(x => x.State == AvailabilityState.Available || x.State == AvailabilityState.RatherNot)
            .GroupBy(x => x.ShiftDay.Date)
            .ToDictionaryAsync(x => x.Key, x => x.ToArray());
    }

    private IQueryable<WorkingAvailability> GetByShiftAndDuration(Shift shift, DateOnly from, DateOnly to)
    {
        return Context.WorkingAvailabilities.Where(x => x.ShiftDay.Shift == shift && x.ShiftDay.Date >= from && x.ShiftDay.Date < to);
    }
}
