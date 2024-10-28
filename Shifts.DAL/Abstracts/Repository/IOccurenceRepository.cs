using Shifts.DAL.Models;

namespace Shifts.DAL.Abstracts.Repository;
public interface IOccurenceRepository : IRepository<Occurence, long>, IAsyncRepository<Occurence, long>
{
    Task<IDictionary<DateOnly, Occurence[]>> GetAllByShiftAndDuration(Shift shift, DateOnly from, DateOnly to);
}
