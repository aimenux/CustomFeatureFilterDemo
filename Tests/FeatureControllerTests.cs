using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Tests;

public class FeatureControllerTests
{
    [Fact]
    public async Task ShouldAwesomeFeatureBeEnabledForAllowedBrowser()
    {
        // arrange
        var fixture = new WebApiTestFixture
        {
            UserAgent = "AwesomeBrowser"
        };

        var client = fixture.CreateClient();

        // act
        var response = await client.GetAsync("/feature/list");
        var responseBody = await response.Content.ReadAsStringAsync();

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseBody.Should().Be("[{\"feature\":\"AwesomeFeature\",\"userAgent\":\"AwesomeBrowser\",\"isEnabled\":true}]");
    }

    [Fact]
    public async Task ShouldAwesomeFeatureBeDisabledForNotAllowedBrowser()
    {
        // arrange
        var fixture = new WebApiTestFixture
        {
            UserAgent = "UnknownBrowser"
        };

        var client = fixture.CreateClient();

        // act
        var response = await client.GetAsync("/feature/list");
        var responseBody = await response.Content.ReadAsStringAsync();

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseBody.Should().Be("[{\"feature\":\"AwesomeFeature\",\"userAgent\":\"UnknownBrowser\",\"isEnabled\":false}]");
    }
}