using Redirector.Api.Routes;
using Redirector.App;

var builder = WebApplication.CreateBuilder(args);
var sources = builder.Configuration.GetSection("readRulesFrom").Get<RuleSources>();
if(sources?.FileConfig is not null)
{
    var fileConfig = sources.FileConfig;
    if(string.Equals(".json", Path.GetExtension(fileConfig), StringComparison.OrdinalIgnoreCase))
    {
        builder.Configuration.AddJsonFile(fileConfig);
    }
    else if (
        string.Equals(".yaml", Path.GetExtension(fileConfig), StringComparison.OrdinalIgnoreCase)
        || string.Equals(".yml", Path.GetExtension(fileConfig), StringComparison.OrdinalIgnoreCase))
    {
        builder.Configuration.AddYamlFile(fileConfig);
    }
}

var app = builder.Build();
app.MapRedirectRoutes();
app.MapFallback(() => "Hello!");
await app.RunAsync();
