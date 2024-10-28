using Shifts.DAL.Models;

namespace Shifts.DAL.Abstracts.Repository;
public interface IWorkingAvailabilityRepository : IRepository<WorkingAvailability, long>, IAsyncRepository<WorkingAvailability, long>
{
    Task<IEnumerable<ApplicationUser>> GetAvailableUsersByShiftAndDurationAsync(Shift shift, DateOnly from, DateOnly to);

    Task<Dictionary<DateOnly, WorkingAvailability[]>> GetWorkingAvailabilitiesByShiftAndDurationAsync(Shift shift, DateOnly from, DateOnly to);
}
