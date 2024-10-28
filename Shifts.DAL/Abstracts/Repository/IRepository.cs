using System.Linq.Expressions;

namespace Shifts.DAL.Abstracts.Repository;
public interface IRepository<T, R> where T : IModel<R> where R : IEquatable<R>
{
    T GetById(R id);

    IEnumerable<T> GetAll();

    IEnumerable<T> GetByParameter<TParam>(Expression<Func<T, TParam>> getter, TParam value) where TParam : IEquatable<TParam>;

    void Create(T model);

    void Delete(R id);

    void Save();
}
