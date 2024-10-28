namespace Shared.Extensions;
public static class DictionryExtensions
{
    public static R? GetValueOrDefault<T, R>(this IDictionary<T, R> dict, T key, R? defaultValue = default)
    {
        if (dict.ContainsKey(key))
            return dict[key];

        return defaultValue;
    }
}
