using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using Shifts.DAL.Abstracts.Repository;
using Shifts.DAL.Models;

namespace Shifts.DAL.Repository;
public class ShiftRepository : RepositoryBase<Shift, long>, IShiftRepository
{
    public ShiftRepository(ApplicationDbContext ctx) : base(ctx)
    {
    }

    public async Task<IEnumerable<Shift>> GetAllValidAsync(DateOnly month)
    {
        var date = month.GetFirstInMonth();
        return await Context.Shifts.Where(x => x.Start <= date && x.End.GetValueOrMax() >= date).ToArrayAsync();
    }
}
