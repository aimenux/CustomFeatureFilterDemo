using Microsoft.FeatureManagement;
using WebApi.Helpers;

namespace WebApi.Filters;

[FilterAlias("Browser")]
public class BrowserFilter : IFeatureFilter
{
    private readonly IUserAgentHelper _userAgentHelper;

    public BrowserFilter(IUserAgentHelper userAgentHelper)
    {
        _userAgentHelper = userAgentHelper ?? throw new ArgumentNullException(nameof(userAgentHelper));
    }

    public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
    {
        var userAgent = _userAgentHelper.GetUserAgent();
        if (string.IsNullOrWhiteSpace(userAgent)) return Task.FromResult(false);
        var settings = context.Parameters.Get<BrowserFilterSettings>();
        return Task.FromResult(settings.AllowedBrowsers.Any(userAgent.Contains));
    }
}