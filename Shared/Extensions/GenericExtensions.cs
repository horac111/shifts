namespace Shared.Extensions;
public static class GenericExtensions
{
    public static T GetValueOrMax<T>(this Nullable<T> value) where T : struct => value ?? GetMaxValue<T>();
    public static T GetValueOrMin<T>(this Nullable<T> value) where T : struct => value ?? GetMinValue<T>();

    private static T GetMaxValue<T>()
    {
        return default(T) switch
        {
            int => (T)(object)int.MaxValue,
            decimal => (T)(object)decimal.MaxValue,
            float => (T)(object)float.MaxValue,
            long => (T)(object)long.MaxValue,
            short => (T)(object)short.MaxValue,
            DateTime => (T)(object)DateTime.MaxValue,
            DateOnly => (T)(object)DateOnly.MaxValue,
            TimeOnly => (T)(object)TimeOnly.MaxValue,
            _ => throw new NotImplementedException()
        };
    }

    private static T GetMinValue<T>()
    {
        return default(T) switch
        {
            int => (T)(object)int.MinValue,
            decimal => (T)(object)decimal.MinValue,
            float => (T)(object)float.MinValue,
            long => (T)(object)long.MinValue,
            short => (T)(object)short.MinValue,
            DateTime => (T)(object)DateTime.MinValue,
            DateOnly => (T)(object)DateOnly.MinValue,
            TimeOnly => (T)(object)TimeOnly.MinValue,
            _ => throw new NotImplementedException()
        };
    }
}
