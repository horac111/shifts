
namespace Shifts.DAL.Abstracts;
public abstract class Model<T> : IModel<T> where T : IEquatable<T>
{
    public virtual T Id { get; set; } = default!;

    public override bool Equals(object? obj)
    {
        return obj is Model<T> model &&
               EqualityComparer<T>.Default.Equals(Id, model.Id);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }
}
