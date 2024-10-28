using Shifts.DAL.Abstracts.Repository;
using Shifts.DAL.Models;

namespace Shifts.DAL.Repository;
public class ShiftAssignmentRepository : RepositoryBase<ShiftAssignment, long>, IShiftAssignmentRepository
{
    public ShiftAssignmentRepository(ApplicationDbContext ctx) : base(ctx)
    {
    }
}
