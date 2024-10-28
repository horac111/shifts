using Microsoft.AspNetCore.Http;

namespace Shared.Extensions;
public static class HttpRequestExtensions
{
    public static UriBuilder CreateBaseUriBuilder(this HttpRequest request)
    {
        UriBuilder builder = new();
        builder.Scheme = request.Scheme;
        builder.Path = request.PathBase;
        builder.Host = request.Host.Host;
        if (request.Host.Port.HasValue)
            builder.Port = request.Host.Port.Value;

        return builder;
    }
}
