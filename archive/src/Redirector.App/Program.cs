using Redirector.Api.Routes;
using Redirector.App.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Configure();
var app = builder.Build();

app.MapRedirectRoutes();
app.MapFallback(() => Results.NotFound());
await app.RunAsync();


public partial class Program { }
