using Redirector.Api.Routes;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapRedirectRoutes();
app.MapFallback(() => "Hello!");
await app.RunAsync();
