namespace Redirector.Api;

public sealed record Rule(
    string Slug,
    Uri RedirectTo
);
