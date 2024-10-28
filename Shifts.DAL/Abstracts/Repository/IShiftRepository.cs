using Shifts.DAL.Models;

namespace Shifts.DAL.Abstracts.Repository;
public interface IShiftRepository : IRepository<Shift, long>, IAsyncRepository<Shift, long>
{
    Task<IEnumerable<Shift>> GetAllValidAsync(DateOnly month);
}
