namespace Redirector.Api.Routes;

public static class RedirectRoutes
{
    public static void MapRedirectRoutes(this WebApplication app)
    {
        var rulesSection = app.Configuration.GetRequiredSection("rules");
        var rules = rulesSection.Get<List<Rule>>() ?? throw new InvalidOperationException("No Rule found");

        foreach(var rule in rules)
        {
            app.MapGet($"/{rule.Slug}", () => Results.Redirect(rule.RedirectTo.AbsoluteUri));
        }
    }
}
