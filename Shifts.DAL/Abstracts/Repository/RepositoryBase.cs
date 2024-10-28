using Microsoft.EntityFrameworkCore;
using Shifts.DAL.Models;
using System.Linq.Expressions;

namespace Shifts.DAL.Abstracts.Repository;
public abstract class RepositoryBase<T, R> : IRepository<T, R>, IAsyncRepository<T, R> where T : class, IModel<R> where R : IEquatable<R>
{
    #region Parameters

    protected ApplicationDbContext Context { get; }

    #endregion

    #region Constructor

    public RepositoryBase(ApplicationDbContext ctx)
    {
        Context = ctx;
    }

    #endregion

    #region Public methods

    public IEnumerable<T> GetAll() => Context.Set<T>().ToArray();

    public T GetById(R id) => Context.Set<T>().First(x => x.Id == null ? false : x.Id.Equals(id));

    public async Task<T> GetByIdAsync(R id) => await Context.Set<T>().FirstAsync(x => x.Id == null ? false : x.Id.Equals(id));

    public async Task<IEnumerable<T>> GetAllAsync() => await Context.Set<T>().ToArrayAsync();

    public async Task<IEnumerable<T>> GetByParameterAsync<TParam>(Expression<Func<T, TParam>> getter, TParam value) where TParam : IEquatable<TParam>
    {
        return await Context.Set<T>().Where(GetExpression(getter, value)).ToArrayAsync();
    }

    public IEnumerable<T> GetByParameter<TParam>(Expression<Func<T, TParam>> getter, TParam value) where TParam : IEquatable<TParam>
    {
        return Context.Set<T>().Where(GetExpression(getter, value)).ToArray();
    }

    public void Create(T model) => Context.Set<T>().Add(model);

    public void Delete(R id) => Context.Set<T>().Remove(GetById(id));

    public void Save() => Context.SaveChanges();

    public async Task SaveAsync() => await Context.SaveChangesAsync();

    #endregion

    #region Private methods

    private Expression<Func<T, bool>> GetExpression<TParam>(Expression<Func<T, TParam>> getter, TParam value)
    {
        var param = Expression.Parameter(typeof(T));
        var valueExpr = Expression.Lambda<Func<T, TParam>>(getter, param);
        var equal = Expression.Equal(valueExpr, Expression.Constant(value));
        return Expression.Lambda<Func<T, bool>>(equal, param);
    }

    #endregion
}
