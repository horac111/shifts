namespace Shared.Extensions;
public static class UriBuilderExtensions
{
    public static void PathAppend(this UriBuilder builder, string path)
    {
        builder.Path += $"/{path}";
    }

    public static void QuerryParameterAppend(this UriBuilder builder, string name, string value)
    {
        string query = string.Empty;
        if (!string.IsNullOrEmpty(builder.Query))
            query = "&";

        query += $"{query}{name}={value}";
    }
}
