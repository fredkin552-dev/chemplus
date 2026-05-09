var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// CORS for frontend -> backend calls (e.g., Netlify calling Render API)
// Set env var: CORS_ORIGIN=https://your-netlify-site.netlify.app
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendCors", policyBuilder =>
    {
        var origin = builder.Configuration["CORS_ORIGIN"];

        if (!string.IsNullOrWhiteSpace(origin))
        {
            policyBuilder.WithOrigins(origin).AllowAnyHeader().AllowAnyMethod();
        }
        else
        {
            // Local/dev fallback
            policyBuilder
                .SetIsOriginAllowed(_ => true)
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Intercept CORS preflight for API before routing so it never becomes 405.
app.Use(async (context, next) =>
{
    if (context.Request.Method.Equals("OPTIONS", StringComparison.OrdinalIgnoreCase) &&
        context.Request.Path.Value.StartsWith("/api", StringComparison.OrdinalIgnoreCase))
    {
        context.Response.StatusCode = StatusCodes.Status204NoContent;

        var origin = context.Request.Headers["Origin"].ToString();
        var allowedOrigin = builder.Configuration["CORS_ORIGIN"];

        // If CORS_ORIGIN is set, only echo it back when it matches.
        if (!string.IsNullOrWhiteSpace(allowedOrigin) &&
            !string.IsNullOrWhiteSpace(origin) &&
            !string.Equals(origin, allowedOrigin, StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        if (!string.IsNullOrWhiteSpace(origin))
        {
            context.Response.Headers["Access-Control-Allow-Origin"] = origin;
            context.Response.Headers["Vary"] = "Origin";
        }

        context.Response.Headers["Access-Control-Allow-Methods"] = "GET,POST,PUT,PATCH,DELETE,OPTIONS";

        var requestedHeaders = context.Request.Headers["Access-Control-Request-Headers"].ToString();
        context.Response.Headers["Access-Control-Allow-Headers"] = string.IsNullOrWhiteSpace(requestedHeaders) ? "*" : requestedHeaders;

        context.Response.Headers["Access-Control-Max-Age"] = "86400";

        return;
    }

    await next();
});

app.UseRouting();
app.UseCors("FrontendCors");
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
