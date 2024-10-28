using Microsoft.EntityFrameworkCore;
using Shifts.DAL.Abstracts.Repository;
using Shifts.DAL.Models;

namespace Shifts.DAL.Repository;
public class OccurenceRepository : RepositoryBase<Occurence, long>, IOccurenceRepository
{
    public OccurenceRepository(ApplicationDbContext ctx) : base(ctx)
    {
    }

    public async Task<IDictionary<DateOnly, Occurence[]>> GetAllByShiftAndDuration(Shift shift, DateOnly from, DateOnly to)
    {
        return await Context.Occurences.Where(x => x.Shift == shift && x.Date >= from && x.Date < to)
            .GroupBy(x => x.Date)
            .ToDictionaryAsync(x => x.Key, x => x.ToArray());
    }
}
