using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureManager _featureManager;
        private readonly IUserAgentHelper _userAgentHelper;
        private readonly ILogger<FeatureController> _logger;

        public FeatureController(IFeatureManager featureManager, IUserAgentHelper userAgentHelper, ILogger<FeatureController> logger)
        {
            _featureManager = featureManager ?? throw new ArgumentNullException(nameof(featureManager));
            _userAgentHelper = userAgentHelper ?? throw new ArgumentNullException(nameof(userAgentHelper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetFeaturesAsync()
        {
            var response = new[]
            {
                new
                {
                    Feature = FeatureFlags.AwesomeFeature,
                    UserAgent = _userAgentHelper.GetUserAgent(),
                    IsEnabled = await _featureManager.IsEnabledAsync(FeatureFlags.AwesomeFeature)
                }
            };

            return new OkObjectResult(response);
        }
    }
}