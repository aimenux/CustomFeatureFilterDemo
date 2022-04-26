using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using WebApi;
using WebApi.Helpers;

namespace Tests;

public class WebApiTestFixture : WebApplicationFactory<Startup>
{
    public string UserAgent { get; set; } = "Undefined";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, configBuilder) =>
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.testing.json");
            configBuilder.AddJsonFile(configPath);
        });

        builder.ConfigureTestServices(services =>
        {
            var userAgentHelper = Substitute.For<IUserAgentHelper>();
            userAgentHelper
                .GetUserAgent()
                .Returns(UserAgent);
            services.AddSingleton<IUserAgentHelper>(_ => userAgentHelper);
        });
    }
}