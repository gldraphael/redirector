namespace Redirector.App.Tests.ConfigTests;

using Redirector.App.Tests.Helpers;

[Collection("Can_read_from_file")]
public class Read_rules_correctly : IClassFixture<TestWebAppFactoryFileFixture>
{
    [Fact]
    public async Task Should_use_rule_from_file()
    {
        // rule defined in file at index 1
        var response = await _httpClient.GetAsync("/chopin");

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.Redirect);
        response.Headers.Location.ShouldBe(new("https://en.wikipedia.org/wiki/Fr%C3%A9d%C3%A9ric_Chopin"));
    }

    [Fact]
    public async Task Env_should_override_file()
    {
        // rule defined in file at index 0 & overriden by env variable
        var response = await _httpClient.GetAsync("/wiki");

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.Redirect);
        response.Headers.Location.ShouldBe(new("https://en.wikipedia.org/wiki/Fr%C3%A9d%C3%A9ric_Chopin"));
    }


    private readonly TestWebAppFactory _factory;
    private readonly HttpClient _httpClient;
    public Read_rules_correctly(TestWebAppFactoryFileFixture factory)
    {
        _factory = factory.Factory;
        _httpClient = _factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
}

public class TestWebAppFactoryFileFixture : IDisposable
{

    public TestWebAppFactory Factory { get; }
    public TestWebAppFactoryFileFixture()
    {
        const string TEST_RULES = "ConfigTests/rules.test.yml";
        Environment.SetEnvironmentVariable("ReadRulesFrom__FileConfig", Path.GetFullPath(TEST_RULES));
        Environment.SetEnvironmentVariable("RULES__0__SLUG", "wiki");
        Environment.SetEnvironmentVariable("RULES__0__REDIRECTTO", "https://en.wikipedia.org/wiki/Fr%C3%A9d%C3%A9ric_Chopin");
        Factory = new ();
    }

    public void Dispose()
    {
        Factory.Dispose();
    }

}
