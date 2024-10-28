using System.Linq.Expressions;

namespace Shifts.DAL.Abstracts.Repository;
public interface IAsyncRepository<T, R> where T : IModel<R> where R : IEquatable<R>
{
    Task<T> GetByIdAsync(R id);

    Task<IEnumerable<T>> GetAllAsync();

    Task<IEnumerable<T>> GetByParameterAsync<TParam>(Expression<Func<T, TParam>> getter, TParam value) where TParam : IEquatable<TParam>;

    Task SaveAsync();
}
