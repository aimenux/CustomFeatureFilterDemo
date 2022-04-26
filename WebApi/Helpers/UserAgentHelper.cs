namespace WebApi.Helpers;

public class UserAgentHelper : IUserAgentHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserAgentHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public string GetUserAgent()
    {
        return _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString() ?? string.Empty;
    }
}