using Shifts.DAL.Models;

namespace Shifts.DAL.Abstracts.Repository;
public interface IShiftAssignmentRepository : IAsyncRepository<ShiftAssignment, long>, IRepository<ShiftAssignment, long>
{
}
