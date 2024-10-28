using Shifts.DAL.Models;

namespace Shifts.DAL.Abstracts.Repository;
public interface IShiftDayRepository : IRepository<ShiftDay, long>, IAsyncRepository<ShiftDay, long>
{
    Task<IEnumerable<ShiftDay>> GetAllByShiftAndDurationAsync(Shift shift, DateOnly from, DateOnly to);
}
