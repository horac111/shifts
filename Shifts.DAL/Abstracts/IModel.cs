namespace Shifts.DAL.Abstracts;
public interface IModel<T> where T : IEquatable<T>
{
    public T Id { get; set; }
}
