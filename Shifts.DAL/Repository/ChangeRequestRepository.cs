using Shifts.DAL.Abstracts.Repository;
using Shifts.DAL.Models;

namespace Shifts.DAL.Repository;
public class ChangeRequestRepository : RepositoryBase<ChangeRequest, long>, IChangeRequestRepository
{
    public ChangeRequestRepository(ApplicationDbContext ctx) : base(ctx)
    {
    }
}
