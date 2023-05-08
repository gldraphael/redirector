namespace Redirector.App.Tests.ConfigTests;

using Redirector.App.Tests.Helpers;

[Collection("Can_read_from_env")]
public class Can_read_rules_from_env : IClassFixture<TestWebAppFactoryEnvFixture>
{
    private readonly TestWebAppFactory _factory;
    private readonly HttpClient _httpClient;

    public Can_read_rules_from_env(TestWebAppFactoryEnvFixture factory)
    {
        _factory = factory.Factory;
        _httpClient = _factory.CreateClient(new  Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions{
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task Should_use_rule_set_by_env_var()
    {
        var response = await _httpClient.GetAsync("/wiki");

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.Redirect);
        response.Headers.Location.ShouldBe(new("https://www.wikipedia.org"));
    }
}

public class TestWebAppFactoryEnvFixture : IDisposable
{

    public TestWebAppFactory Factory { get; }
    public TestWebAppFactoryEnvFixture()
    {
        Environment.SetEnvironmentVariable("RULES__0__SLUG", "wiki");
        Environment.SetEnvironmentVariable("RULES__0__REDIRECTTO", "https://www.wikipedia.org");
        Factory = new TestWebAppFactory();
    }

    public void Dispose()
    {
        Factory.Dispose();
    }

}
