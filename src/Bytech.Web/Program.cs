using Bytech.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

// One address for search engines: https://bytech.se. Traefik terminates TLS and passes on the
// original scheme in X-Forwarded-Proto, so plain-HTTP and www requests are sent to the
// canonical address. Requests without that header (health probes, local runs) pass through.
app.Use(async (context, next) =>
{
    var request = context.Request;
    var host = request.Host.Host;
    var isSiteHost = host.Equals("bytech.se", StringComparison.OrdinalIgnoreCase)
        || host.EndsWith(".bytech.se", StringComparison.OrdinalIgnoreCase);
    var isWww = host.Equals("www.bytech.se", StringComparison.OrdinalIgnoreCase);
    var isPlainHttp = request.Headers["X-Forwarded-Proto"].ToString()
        .Equals("http", StringComparison.OrdinalIgnoreCase);

    if (isSiteHost && (isWww || isPlainHttp)
        && (HttpMethods.IsGet(request.Method) || HttpMethods.IsHead(request.Method))
        && !request.Path.StartsWithSegments("/.well-known"))
    {
        var target = isWww ? "bytech.se" : host;
        context.Response.Redirect($"https://{target}{request.Path}{request.QueryString}", permanent: true);
        return;
    }

    await next();
});

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
