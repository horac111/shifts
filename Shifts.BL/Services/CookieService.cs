using Microsoft.AspNetCore.Http;

namespace Shifts.BL.Services;
public class CookieService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CookieService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetCookie(string key)
    {
        return _httpContextAccessor.HttpContext?.Request.Cookies[key];
    }

    public void SetCookie(string key, string value)
    {
        var response = _httpContextAccessor.HttpContext?.Response;
        if (response is not null)
        {
            response.Cookies.Delete(key);
            response.Cookies.Append(key, value);
        }
    }
}
