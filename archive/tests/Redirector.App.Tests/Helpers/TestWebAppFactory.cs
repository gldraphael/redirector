namespace Redirector.App.Tests.Helpers;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;


public class TestWebAppFactory : WebApplicationFactory<Program>
{
    private Action<IHostBuilder>? _createAction;

    public TestWebAppFactory() { }
    public TestWebAppFactory(Action<IHostBuilder> createAction)
    {
        _createAction = createAction;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        if(_createAction is not null) _createAction(builder);
        return base.CreateHost(builder);
    }
        
}
