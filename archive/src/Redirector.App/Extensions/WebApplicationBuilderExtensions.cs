namespace Redirector.App.Extensions;

internal static class WebApplicationBuilderExtensions
{
    public static void Configure(this WebApplicationBuilder builder)
    {
        builder.Configuration.Sources.Clear();
        builder.Configuration.AddJsonFile("./appsettings.json", optional: false);
        builder.Configuration.AddJsonFile($"./appsettings.{builder.Environment.EnvironmentName}.json", optional: true);
        builder.Configuration.AddEnvironmentVariables();
        var sources = builder.Configuration.GetSection("readRulesFrom").Get<RuleSources>();
        builder.Configuration.Sources.RemoveAt(2);
        if (sources?.FileConfig is not null)
        {
            var fileConfig = sources.FileConfig;
            if (string.Equals(".json", Path.GetExtension(fileConfig), StringComparison.OrdinalIgnoreCase))
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
        builder.Configuration.AddEnvironmentVariables();
    }
}
