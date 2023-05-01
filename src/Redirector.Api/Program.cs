var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapFallback(() => "Hello!");
await app.RunAsync();
