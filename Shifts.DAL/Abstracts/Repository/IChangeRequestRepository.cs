using Shifts.DAL.Models;

namespace Shifts.DAL.Abstracts.Repository;
public interface IChangeRequestRepository : IRepository<ChangeRequest, long>, IAsyncRepository<ChangeRequest, long>
{
}
